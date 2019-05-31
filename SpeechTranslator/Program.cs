using System;
using System.IO;
using System.Threading.Tasks;
using CognitiveServices;
using static CognitiveServices.Services;

namespace SpeechTranslator
{
    class Program
    {
        static string subscriptionKey = "<YourCognitiveServiceSubscriptionKey>";
        static string resourceName = "<YourCognitiveServiceName>";
        static string azureRegion = AzureRegions.EastUs;

        static Services cognitiveServices;
        static async Task Main(string[] args)
        {
            cognitiveServices = new Services(resourceName, subscriptionKey, azureRegion);

            Translation translation = await GetTranslationFromSpeech(
                "audioCH-1.wav",
                LanguageLocale.ChineseMainland, 
                Language.English, 
                LanguageForSpeech.EnglishFemale);

            Console.WriteLine($"Final result. Recognized text: {translation.text}.");
            await WriteStreamToFile("translatedResult.wav", translation.audio);

            Console.ReadLine();
        }

        private static async Task WriteStreamToFile(string fileName, Stream dataStreamResult)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                await dataStreamResult.CopyToAsync(fileStream).ConfigureAwait(false);
                fileStream.Close();
            }
            Console.WriteLine($"{fileName} is ready within the debug\\netcoreapp2.2 folder.");
        }
    }
}