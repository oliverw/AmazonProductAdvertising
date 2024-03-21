namespace Nager.AmazonProductAdvertising.Auth
{
    /// <summary>
    /// Amazon Signer for Paapi 5
    /// </summary>
    public interface IAwsSigner
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
        string CreateAuthorizationHeader(DateTime date, string accessKey, string secretKey, Dictionary<string, string> requestHeaders, string httpMethod, string path, string payload, string region, string service);
    }
}
