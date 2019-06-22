using CognitiveServices;
using System;
using System.IO;
using static CognitiveServices.Services;

namespace SpeechToTextStream
{
    class Program
    {
        static string subscriptionKey = "<YourCognitiveServiceSubscriptionKey>";
        static string resourceName = "<YourCognitiveServiceName>";
        static string azureRegion = AzureRegions.EastUs;

        static Services cognitiveServices;

        public static object LanguageLocale { get; private set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Trying the continuous speech to text service");

            string fileName = "audio-en-US-1.wav";
            FileStream stream = new FileStream(fileName, FileMode.Open);
            cognitiveServices = new Services(resourceName, subscriptionKey, azureRegion);
            string result = cognitiveServices.GetTextFromSpeech(Services.LanguageLocale.EnglishUnitedStates, stream).GetAwaiter().GetResult();

            Console.WriteLine($"The result is\n: {result}");

            Console.ReadLine();
        }
    }
}