using CognitiveServices;
using System;
using static CognitiveServices.Services;

namespace SpeechToTextLive
{
    class Program
    {
        //static string subscriptionKey = "<YourCognitiveServiceSubscriptionKey>";
        //static string resourceName = "<YourCognitiveServiceName>";
        //static string azureRegion = AzureRegions.EastUs;
        static string subscriptionKey = "0f460ebe13ff4b26bf6b1540a4f28f81";
        static string resourceName = "faurecia-cognitiveservices";
        static string azureRegion = AzureRegions.EastUs;

        static Services cognitiveServices;
        static void Main(string[] args)
        {
            Console.WriteLine("Trying the continuous speech to text service");

            cognitiveServices = new Services(resourceName, subscriptionKey, azureRegion);
            cognitiveServices.GetTextFromSpeech(LanguageLocale.SpanishMexico);
            cognitiveServices.GetTextFromSpeech(LanguageLocale.EnglishUnitedStates);

            Console.WriteLine("Does the program ever reach this line?");

            Console.ReadLine();
        }
    }
}
