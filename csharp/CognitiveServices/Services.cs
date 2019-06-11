using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServices
{

    public partial class Services
    {
        public async Task<Stream> GetSpeechFromText(string textToSpeech, string language)
        {
            Services.cognitiveServicesAccessToken  = await Authentication.GetAccessToken();

            string message = string.Format(textToSpeechBody, language , textToSpeech);

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(textToSpeechhost);
                    request.Content = new StringContent(message, Encoding.UTF8, "application/ssml+xml");
                    request.Headers.Add("Authorization", "Bearer " + cognitiveServicesAccessToken);
                    request.Headers.Add("Connection", "Keep-Alive");
                    request.Headers.Add("User-Agent", cognitiveServicesResourceName);
                    request.Headers.Add("X-Microsoft-OutputFormat", "riff-24khz-16bit-mono-pcm");
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStreamAsync();
                }
            }
        }

        public async Task<string> GetTextFromSpeech(string fileName)
        {
            var config = SpeechConfig.FromSubscription(cognitiveServicesSubscriptionKey, cognitiveServicesAzureRegion);
            
            var stopRecognition = new TaskCompletionSource<int>();

            string result = string.Empty;
            using (var audioInput = Helper.OpenWavFile(fileName))
            {
                using (var recognizer = new SpeechRecognizer(config, audioInput))
                {
                    recognizer.Recognized += (s, e) =>
                    {
                        if (e.Result.Reason == ResultReason.RecognizedSpeech)
                        {
                            result = e.Result.Text;
                        }
                        else if (e.Result.Reason == ResultReason.NoMatch)
                        {
                            Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                        }
                    };

                    recognizer.Canceled += (s, e) =>
                    {
                        if (e.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                        stopRecognition.TrySetResult(0);
                    };

                    await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                    Task.WaitAny(new[] { stopRecognition.Task });
                    await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);

                    return result;
                }
            }
        }

        public async Task<Translation> GetTranslationFromSpeech(string fileName, 
            string fromLanguageLocale, 
            string targetLanguage, 
            string voiceLanguage)
        {
            Translation translation = new Translation();

            var config = SpeechTranslationConfig.FromSubscription(Services.cognitiveServicesSubscriptionKey, Services.cognitiveServicesAzureRegion);

            var stopRecognition = new TaskCompletionSource<int>();

            config.SpeechRecognitionLanguage = fromLanguageLocale;
            config.AddTargetLanguage(targetLanguage);
            config.VoiceName = voiceLanguage;

            using (var audioInput = Helper.OpenWavFile(fileName))
            {
                using (var recognizer = new TranslationRecognizer(config, audioInput))
                {
                    recognizer.Synthesizing += (s, e) =>
                    {
                        var audio = e.Result.GetAudio();
                        if (audio.Length > 0)
                        {
                            translation.audio = new MemoryStream(audio);
                        }
                    };

                    recognizer.Recognized += (s, e) =>
                    {
                        if (e.Result.Reason == ResultReason.TranslatedSpeech)
                        {
                            foreach (var element in e.Result.Translations)
                            {
                                translation.text = element.Value;
                            }
                        }
                    };

                    recognizer.Canceled += (s, e) =>
                    {
                        if (e.Reason != CancellationReason.EndOfStream)
                        {
                            Console.WriteLine($"\nRecognition canceled. Reason: {e.Reason}; ErrorDetails: {e.ErrorDetails}");
                        }
                        stopRecognition.TrySetResult(0);
                    };

                    await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                    Task.WaitAny(new[] { stopRecognition.Task });
                    await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);

                    return translation;
                }
            }
        }
    }
}