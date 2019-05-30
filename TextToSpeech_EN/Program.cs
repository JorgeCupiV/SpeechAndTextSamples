using System;
using System.IO;
using System.Threading.Tasks;
using CognitiveServices;
using static CognitiveServices.Services;

namespace TextToSpeech_EN
{
    class Program
    {
        #region Text with options
        static string option1 = "Hello, I'm David, can you hear me?";
        static string option2 = "Hi, I can hear you loud and clear, these sound bubbles are awesome!";
        static string option3 = "Good morning everyone, let's wait 5 minutes for the rest of the team to join";
        static string option4 = "Just FYI, I'm entering into a tunnel in case you stop hearing me for a couple of seconds";
        static string option5 = "I got Paul from my team that can prepare the slides for the presentation tomorrow, should we have a meeting later today to look at it?";
        static string option6 = "Hi Mary, after what we saw last week on our 1:1 I took the liberty to pick the best ideas to pitch them to the rest of the team later this week, I'll send an email containing my draft in 10 minutes so you can take a look at it and give it a green flag";
        #endregion

        static string subscriptionKey = "<YourCognitiveServiceSubscriptionKey>";
        static string resourceName = "<YourCognitiveServiceName>";
        static string azureRegion = AzureRegions.EastUs;

        static Services cognitiveServices;
        static void Main(string[] args)
        {
            cognitiveServices = new Services(resourceName, subscriptionKey,azureRegion);

            bool appIsRunning = true;
            while (appIsRunning)
            {
                string textToTranslate = string.Empty;

                string option = ShowMenuAndSelectOption();
                switch (option)
                {
                    case "1":
                        textToTranslate = option1;
                        break;
                    case "2":
                        textToTranslate = option2;
                        break;
                    case "3":
                        textToTranslate = option3;
                        break;
                    case "4":
                        textToTranslate = option4;
                        break;
                    case "5":
                        textToTranslate = option5;
                        break;
                    case "6":
                        textToTranslate = option6;
                        break;
                    case "7":
                        Console.Write("Please write what you want to convert to speech:");
                        textToTranslate = Console.ReadLine();
                        break;
                    case "8":
                        appIsRunning = false;
                        break;
                    default:
                        break;
                }

                if (appIsRunning)
                {
                    ApplyTextToSpeechTo(textToTranslate);

                    Console.Clear();
                    Console.WriteLine($"Option {option} selected. Press Enter to go back to the menu again");
                    Console.ReadKey();
                }
            }
        }

        private static string ShowMenuAndSelectOption()
        {
            Console.WriteLine("Choose your text to convert or select option 8 to exit");
            Console.WriteLine($"1. '{option1}'");
            Console.WriteLine($"2. '{option2}'");
            Console.WriteLine($"3. '{option3}'");
            Console.WriteLine($"4. '{option4}'");
            Console.WriteLine($"5. '{option5}'");
            Console.WriteLine($"6. '{option6}'");
            Console.WriteLine($"7. Enter your own text");
            Console.WriteLine($"8. Exit");
            return Console.ReadLine();
        }

        private static async void ApplyTextToSpeechTo(string textToTranslate)
        {
            var dataStreamResult = await cognitiveServices.GetSpeechFromText(textToTranslate, Languages.EnglishNeuralFemale);
            await WriteStreamToFile("result.wav", dataStreamResult);

            dataStreamResult = await cognitiveServices.GetSpeechFromText(textToTranslate, Languages.ChineseMainlandNeuralFemale);
            await WriteStreamToFile("resultInChinese.wav", dataStreamResult);
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