using System.Security.Cryptography;
using System.Text;

namespace Nager.AmazonProductAdvertising.Auth
{
    /// <summary>
    /// Amazon Signer for Paapi 5
    /// </summary>
    public class AwsSigner : IAwsSigner
    {
        /// <summary>
        /// Create Authorization Header
        /// </summary>
        /// <param name="date"></param>
        /// <param name="accessKey"></param>
        /// <param name="secretKey"></param>
        /// <param name="requestHeaders"></param>
        /// <param name="httpMethod"></param>
        /// <param name="path"></param>
        /// <param name="payload"></param>
        /// <param name="region"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public string CreateAuthorizationHeader(DateTime date, string accessKey, string secretKey, Dictionary<string, string> requestHeaders, string httpMethod, string path, string payload, string region, string service)
        {
            var signedHeaders = CreateSignedHeaders(requestHeaders);
            var canonicalRequest = CreateCanonicalRequest(httpMethod, path, new Dictionary<string, string>(), requestHeaders, payload);
            var stringToSign = CreateStringToSign(date, region, service, canonicalRequest);
            var signature = CreateSignature(secretKey, date, region, service, stringToSign);
            var authorizationHeader = CreateAuthorizationHeaders(date, accessKey, region, service, signedHeaders, signature);
            return authorizationHeader;
        }

        private string CreateAuthorizationHeaders(DateTime timestamp, string accessKey, string region, string service, string signedHeaders, string signature) =>
            $"AWS4-HMAC-SHA256 Credential={accessKey}/{CreateCredentialScope(timestamp, region, service)}, SignedHeaders={signedHeaders}, Signature={signature}";


        private string CreateCanonicalRequest(string method, string pathname, Dictionary<string, string> query, Dictionary<string, string> headers, string payload)
        {
            return string.Join("\n",
                method.ToUpper(),
                pathname,
                CreateCanonicalQueryString(query),
                CreateCanonicalHeaders(headers),
                CreateSignedHeaders(headers),
                HexEncodedHash(payload));
        }

        private string CreateCanonicalQueryString(Dictionary<string, string> parameters)
        {
            var sortedParams = parameters.OrderBy(p => p.Key);
            return string.Join("&",
                sortedParams.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
        }

        private string CreateCanonicalHeaders(Dictionary<string, string> headers)
        {
            return string.Concat(
                headers.OrderBy(h => h.Key.ToLowerInvariant())
                       .Select(h => $"{h.Key.ToLowerInvariant().Trim()}:{h.Value.Trim()}\n"));
        }

        private string CreateSignedHeaders(Dictionary<string, string> headers)
        {
            return string.Join(";",
                headers.Keys.Select(x => x.ToLower().Trim()).OrderBy(k => k));
        }

        private string CreateCredentialScope(DateTime time, string region, string service) => $"{ToDate(time)}/{region}/{service}/aws4_request";


        private string CreateStringToSign(DateTime time, string region, string service, string request)
        {
            return string.Join("\n",
                "AWS4-HMAC-SHA256",
                time.ToAmzDateStr(),
                CreateCredentialScope(time, region, service),
                HexEncodedHash(request));
        }

        private string CreateSignature(string secret, DateTime time, string region, string service, string stringToSign)
        {
            var h1 = Hmac(Encoding.UTF8.GetBytes("AWS4" + secret), ToDate(time)); // date-key
            var h2 = Hmac(h1, region); // region-key
            var h3 = Hmac(h2, service); // service-key
            var h4 = Hmac(h3, "aws4_request"); // signing-key
            return HmacHex(h4, stringToSign);
        }

        private string ToTime(DateTime time)
        {
            return time.ToString("yyyyMMddTHHmmssZ");
        }

        private string ToDate(DateTime time)
        {
            return time.ToString("yyyyMMdd");
        }

        private byte[] Hmac(byte[] key, string data)
        {
            using(var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }

        private string HmacHex(byte[] key, string data)
        {
            using(var hmac = new HMACSHA256(key))
            {
                return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
            }
        }

        public string HexEncodedHash(string data)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
