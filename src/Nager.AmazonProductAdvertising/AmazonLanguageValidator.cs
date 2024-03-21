
namespace Nager.AmazonProductAdvertising
{
    public class AmazonLanguageValidator
    {
        private readonly Dictionary<LanguageCodes, AmazonEndpoint[]> Languages = new Dictionary<LanguageCodes, AmazonEndpoint[]>
            {
                { LanguageCodes.cs_CZ, new [] { AmazonEndpoint.DE } },
                { LanguageCodes.de_DE, new [] { AmazonEndpoint.DE, AmazonEndpoint.US } },
                { LanguageCodes.en_GB, new [] { AmazonEndpoint.DE, AmazonEndpoint.UK } },
                { LanguageCodes.nl_NL, new [] { AmazonEndpoint.DE, AmazonEndpoint.NL } },
                { LanguageCodes.pl_PL, new [] { AmazonEndpoint.DE } },
                { LanguageCodes.tr_TR, new [] { AmazonEndpoint.DE, AmazonEndpoint.TR } },
                { LanguageCodes.en_AU, new [] { AmazonEndpoint.AU } },
                { LanguageCodes.pt_BR, new [] { AmazonEndpoint.BR, AmazonEndpoint.US } },
                { LanguageCodes.en_CA, new [] { AmazonEndpoint.CA } },
                { LanguageCodes.fr_CA, new [] { AmazonEndpoint.CA } },
                { LanguageCodes.fr_FR, new [] { AmazonEndpoint.FR } },
                { LanguageCodes.en_IN, new [] { AmazonEndpoint.IN } },
                { LanguageCodes.it_IT, new [] { AmazonEndpoint.IT } },
                { LanguageCodes.ja_JP, new [] { AmazonEndpoint.JP } },
                { LanguageCodes.zh_CN, new [] { AmazonEndpoint.JP, AmazonEndpoint.US } },
                { LanguageCodes.es_MX, new [] { AmazonEndpoint.MX } },
                { LanguageCodes.en_SG, new [] { AmazonEndpoint.SG } },
                { LanguageCodes.es_ES, new [] { AmazonEndpoint.ES } },
                { LanguageCodes.en_AE, new [] { AmazonEndpoint.AE } },
                { LanguageCodes.ar_AE, new [] { AmazonEndpoint.AE } },
                { LanguageCodes.en_US, new [] { AmazonEndpoint.JP, AmazonEndpoint.US } },
                { LanguageCodes.es_US, new [] { AmazonEndpoint.US } },
                { LanguageCodes.ko_KR, new [] { AmazonEndpoint.US } },
                { LanguageCodes.zh_TW, new [] { AmazonEndpoint.US } },
            };


        /// <summary>
        /// Check languages are valid for endpoint
        /// See https://webservices.amazon.com/paapi5/documentation/locale-reference.html for more information.
        /// </summary>
        /// <param name="languages"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public bool IsLanguageValid(LanguageCodes[] languages, AmazonEndpoint endpoint)
        {
            if(languages == null)
            {
                return false;
            }

            var items = Languages.Count(language =>
                languages.Any(x => language.Key.Equals(x) &&
                language.Value.Any(y => y.Equals(endpoint)))
            );

            return items == languages.Length;
        }
    }
}
