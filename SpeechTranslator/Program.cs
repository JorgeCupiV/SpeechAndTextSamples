using System;
using System.IO;
using System.Threading.Tasks;
using CognitiveServices;
using static CognitiveServices.Services;

namespace SpeechTranslator
{
    class Program
    {
        static string option1 = "Audio: Hello, I'm David, can you hear me?";
        static string option2 = "Audio: Hi, I can hear you loud and clear, these sound bubbles are awesome!";
        static string option3 = "Audio: Good morning everyone, let's wait 5 minutes for the rest of the team to join";
        static string option4 = "Audio: Just FYI, I'm entering into a tunnel in case you stop hearing me for a couple of seconds";
        static string option5 = "Audio: I got Paul from my team that can prepare the slides for the presentation tomorrow, should we have a meeting later today to look at it?";
        static string option6 = "Audio: Hi Mary, after what we saw last week on our 1:1 I took the liberty to pick the best ideas to pitch them to the rest of the team later this week, I'll send an email containing my draft in 10 minutes so you can take a look at it and give it a green flag";

        static string subscriptionKey = "<YourCognitiveServiceSubscriptionKey>";
        static string resourceName = "<YourCognitiveServiceName>";
        static string azureRegion = AzureRegions.EastUs;

        static Services cognitiveServices;
        static async Task Main(string[] args)
        {
            cognitiveServices = new Services(resourceName, subscriptionKey, azureRegion);
            bool appIsRunning = true;
            while (appIsRunning)
            {
                string audioToConvertToText = "audio-{0}-{1}.wav";
                int option = ShowMenuAndSelectOption();
                string languageOrigin = ShowMenuForLanguageOrigin();
                string [] languageDestiny = ShowMenuForLanguageDestiny();
                if (option <= 6)
                { 
                    audioToConvertToText = string.Format(audioToConvertToText, languageOrigin, option);
                }
                else
                    break;

                Translation translation = await GetTranslationFromSpeech(
                audioToConvertToText,
                languageOrigin,
                languageDestiny[0],
                languageDestiny[1]);

                Console.WriteLine($"Final result. Recognized text: {translation.text}.");
                await WriteStreamToFile("translatedResult.wav", translation.audio);

                Console.ReadLine();
            }
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

        private static string[] ShowMenuForLanguageDestiny()
        {
            Console.WriteLine("Choose your language of destiny:");
            Console.WriteLine($"1. 'Chinese (Mainland)'");
            Console.WriteLine($"2. 'English (United States)'");
            Console.WriteLine($"3. 'French'");
            Console.WriteLine($"4. 'German'");
            Console.WriteLine($"5. 'Hindi'");
            Console.WriteLine($"6. 'Italian'");
            Console.WriteLine($"7. 'Japanese'");
            Console.WriteLine($"8. 'Portuguese (Brazil)'");
            Console.WriteLine($"9. 'Spanish (Mexico)'");
            int languageChosen = int.Parse(Console.ReadLine());
            string[] languages = new string[2];
            switch (languageChosen)
            {
                case 1:
                    languages[0] = Language.ChineseSimplified;
                    languages[1] = LanguageForSpeech.ChineseMainlandNeuralFemale;
                    break;
                case 2:
                    languages[0] = Language.English;
                    languages[1] = LanguageForSpeech.EnglishFemale;
                    break;
                case 3:
                    languages[0] = Language.French;
                    languages[1] = LanguageForSpeech.FrenchFemale;
                    break;
                case 4:
                    languages[0] = Language.German;
                    languages[1] = LanguageForSpeech.GermanFemale;
                    break;
                case 5:
                    languages[0] = Language.Hindi;
                    languages[1] = LanguageForSpeech.HindiFemale;
                    break;
                case 6:
                    languages[0] = Language.Italian;
                    languages[1] = LanguageForSpeech.ItalianFemale;
                    break;
                case 7:
                    languages[0] = Language.Japanese;
                    languages[1] = LanguageForSpeech.JapaneseFemale;
                    break;
                case 8:
                    languages[0] = Language.Portuguese;
                    languages[1] = LanguageForSpeech.PortugueseBrazilFemale;
                    break;
                case 9:
                    languages[0] = Language.Spanish;
                    languages[1] = LanguageForSpeech.SpanishMexicoFemale;
                    break;
            }
            return languages;
        }

        private static string ShowMenuForLanguageOrigin()
        {
            Console.WriteLine("We have two versions, choose one");
            Console.WriteLine($"1. English audio");
            Console.WriteLine($"2. Chinese audio");
            return (int.Parse(Console.ReadLine()) == 1)? 
                LanguageLocale.EnglishUnitedStates : LanguageLocale.ChineseMainland;
        }

        private static int ShowMenuAndSelectOption()
        {
            Console.WriteLine("Choose your audio to translate or select option 8 to exit");
            Console.WriteLine($"1. '{option1}'");
            Console.WriteLine($"2. '{option2}'");
            Console.WriteLine($"3. '{option3}'");
            Console.WriteLine($"4. '{option4}'");
            Console.WriteLine($"5. '{option5}'");
            Console.WriteLine($"6. '{option6}'");
            Console.WriteLine($"7. Exit");
            return int.Parse(Console.ReadLine());
        }
    }
}