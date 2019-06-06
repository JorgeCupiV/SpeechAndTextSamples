namespace CognitiveServices
{
    public partial class Services
    {
        internal static string cognitiveServicesResourceName;
        internal static string cognitiveServicesSubscriptionKey;
        internal static string cognitiveServicesAccessToken;
        internal static string cognitiveServicesAzureRegion;
        internal static string textToSpeechhost = "https://{0}.tts.speech.microsoft.com/cognitiveservices/v1";
        internal static string textToSpeechBody = "<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xml:lang='en-US'>" +
    "<voice name='{0}'>{1}</voice></speak>";


        public Services(string resourceName, string subscriptionKey, string azureRegion)
        {
            cognitiveServicesResourceName = resourceName;
            cognitiveServicesSubscriptionKey = subscriptionKey;
            cognitiveServicesAzureRegion = azureRegion;
        }

        public struct AzureRegions
        {
            public const string EastUs = "eastus";
            public const string WestUs = "westus";
        }

        public struct LanguageForSpeech
        {
            public const string ChineseMainlandFemale = "zh-CN-HuihuiRUS";
            public const string ChineseMainlandNeuralFemale = "zh-CN-XiaoxiaoNeural";
            public const string EnglishFemale = "en-US-ZiraRUS";
            public const string EnglishNeuralFemale = "en-US-JessaNeural";
            public const string FrenchFemale = "fr-FR-Julie-Apollo";
            public const string GermanFemale = "de-DE-Hedda";
            public const string GermanNeuralFemale = "de-DE-KatjaNeural";
            public const string HindiFemale = "hi-IN-Kalpana";
            public const string ItalianNeuralFemale = "it-IT-ElsaNeural";
            public const string ItalianFemale = "it-IT-LuciaRUS";
            public const string JapaneseFemale = "ja-JP-Ayumi-Apollo";
            public const string PortugueseBrazilFemale = "pt-BR-HeloisaRUS";
            public const string PortuguesePortugalFemale = "pt-PT-HeliaRUS";
            public const string SpanishMexicoFemale = "es-MX-HildaRUS";
            public const string SpanishSpainFemale = "es-ES-HelenaRUS";
        }

        public struct Language
        {
            public const string ChineseSimplified = "zh-Hans";
            public const string ChineseTraditional = "zh-Hant";
            public const string English = "en";
            public const string French = "fr";
            public const string German= "de";
            public const string Hindi = "hi";
            public const string Italian = "it";
            public const string Japanese = "ja";
            public const string Portuguese = "pt";
            public const string Spanish = "es";
        }

        public struct LanguageLocale
        {
            public const string ChineseMainland = "zh-CN";
            public const string ChineseHongKong = "zh-HK";
            public const string EnglishUnitedStates = "en-US";
            public const string EnglishUnitedKingdom = "en-UK";
            public const string EnglishCanada = "en-CA";
            public const string FrenchFrance = "fr-FR";
            public const string GermanGermany = "de-DE";
            public const string HindiIndia = "hi-IN";
            public const string ItalianItaly = "it-IT";
            public const string JapaneseJapan = "ja-JP";
            public const string PortuguesePortugal = "pt-PT";
            public const string PortugueseBrazil = "pt-BR";
            public const string SpanishSpain = "es-ES";
            public const string SpanishMexico = "es-MX";
        }
    }
}