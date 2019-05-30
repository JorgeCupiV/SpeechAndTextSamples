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

        public struct Languages
        {
            public const string EnglishFemale = "en-US-ZiraRUS";
            public const string ChineseMainlandFemale = "zh-CN-HuihuiRUS";
            public const string EnglishNeuralFemale = "en-US-JessaNeural";
            public const string ChineseMainlandNeuralFemale = "zh-CN-XiaoxiaoNeural";
        }

        public struct AzureRegions
        {
            public const string EastUs = "eastus";
            public const string WestUs = "westus";
        }
    }
}