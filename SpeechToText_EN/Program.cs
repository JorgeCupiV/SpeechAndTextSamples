using System;
using System.Threading.Tasks;
using CognitiveServices;
using static CognitiveServices.Services;

namespace SpeechToText_EN
{
    class Program
    {
        #region Audio file name and menu options
        static string option1 = "Audio: Hello, I'm David, can you hear me?";
        static string option2 = "Audio: Hi, I can hear you loud and clear, these sound bubbles are awesome!";
        static string option3 = "Audio: Good morning everyone, let's wait 5 minutes for the rest of the team to join";
        static string option4 = "Audio: Just FYI, I'm entering into a tunnel in case you stop hearing me for a couple of seconds";
        static string option5 = "Audio: I got Paul from my team that can prepare the slides for the presentation tomorrow, should we have a meeting later today to look at it?";
        static string option6 = "Audio: Hi Mary, after what we saw last week on our 1:1 I took the liberty to pick the best ideas to pitch them to the rest of the team later this week, I'll send an email containing my draft in 10 minutes so you can take a look at it and give it a green flag";
        #endregion

        static string subscriptionKey = "<YourCognitiveServiceSubscriptionKey>";
        static string resourceName = "<YourCognitiveServiceName>";
        static string azureRegion = AzureRegions.EastUs;

        static Services cognitiveServices;

        static async Task Main(string[] args)
        {
            cognitiveServices = new Services(resourceName, subscriptionKey, AzureRegions.EastUs);
            bool appIsRunning = true;
            while (appIsRunning)
            {
                string audioToConvertToText = "audio{0}.wav";
                int option = ShowMenuAndSelectOption();
                if (option <= 6)
                    audioToConvertToText = string.Format(audioToConvertToText, option);
                else
                { 
                    appIsRunning = false;
                    break;
                }

                string result = await cognitiveServices.GetTextFromSpeech(audioToConvertToText);
                Console.Clear();
                Console.WriteLine($"Option {option} selected. Look at the Speech To Text results:\n{result}");
                Console.ReadKey();
            }
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
