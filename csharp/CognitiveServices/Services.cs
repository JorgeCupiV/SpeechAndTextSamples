using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
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
        /// <summary>
        /// Method that converts text to audio
        /// </summary>
        /// <param name="text">The text string that needs to be converted to speech.</param>
        /// <param name="language">
        /// Receives a LanguageForSpeech as a parameter. <para />
        /// Example: 'zh-CN-HuihuiRUS' for a female speaking in Chinese Mainland, 'en-US-ZiraRUS' for a female speaking english.<para />
        /// Use the LanguageForSpeech struct at the Services class to choose a proper language.
        /// </param>
        /// <returns>Returns a Stream object containing audio obtained from the text sent</returns>
        public async Task<Stream> GetSpeechFromText(string text, string language)
        {
            cognitiveServicesAccessToken  = await Authentication.GetAccessToken();

            string message = string.Format(textToSpeechBody, language , text);

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(textToSpeechServiceUri);
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

        /// <summary>
        /// Method that translates an original audio file to a text and a new audio stream
        /// </summary>
        /// <param name="fileName">Filepath of a WAV Fileparam>
        /// <param name="languageLocale">
        /// Receives a Language locale as a parameter. <para/>
        /// Example: 'en-US' for english (United States), 'es-ES' for spanish (Spain)<para/>
        /// Use the LanguageLocale struct at the Services class to choose a proper language.<para/>
        /// </param>
        /// <param name="targetLanguage">
        /// Receives a Language as a parameter. <para/>
        /// Example: 'en' for english, 'es' for spanish<para/>
        /// Use the Language struct at the Services class to choose a proper language.<para/>
        /// </param>
        /// <param name="voiceLanguage">
        /// Receives a LanguageForSpeech as a parameter. <para />
        /// Example: 'zh-CN-HuihuiRUS' for a female speaking in Chinese Mainland, 'en-US-ZiraRUS' for a female speaking english.<para />
        /// Use the LanguageForSpeech struct at the Services class to choose a proper language.<para/>
        /// </param>
        /// <returns>A Translation object that contains a Text and Stream with the translation</returns>
        public async Task<Translation> GetTranslationFromSpeech(string fileName,
                                            string languageLocale,
                                            string targetLanguage,
                                            string voiceLanguage)
        {
            using (var audioInput = Helper.OpenWavFile(fileName))
            {
                return await Translate(languageLocale, targetLanguage, voiceLanguage, audioInput);
            }
        }

        /// <summary>
        /// Method that translates an original audio stream to a text and a new audio stream
        /// </summary>
        /// <param name="stream">A Stream containing audio</param>
        /// <param name="languageLocale">
        /// Receives a Language locale as a parameter. <para/>
        /// Example: 'en-US' for english (United States), 'es-ES' for spanish (Spain)<para/>
        /// Use the LanguageLocale struct at the Services class to choose a proper language.<para/>
        /// </param>
        /// <param name="targetLanguage">
        /// Receives a Language as a parameter. <para/>
        /// Example: 'en' for english, 'es' for spanish<para/>
        /// Use the Language struct at the Services class to choose a proper language.<para/>
        /// </param>
        /// <param name="voiceLanguage">
        /// Receives a LanguageForSpeech as a parameter. <para />
        /// Example: 'zh-CN-HuihuiRUS' for a female speaking in Chinese Mainland, 'en-US-ZiraRUS' for a female speaking english.<para />
        /// Use the LanguageForSpeech struct at the Services class to choose a proper language.<para/>
        /// </param>
        /// <returns>A Translation object that contains a Text and Stream with the translation</returns>
        public async Task<Translation> GetTranslationFromSpeech(Stream stream,
                                            string languageLocale,
                                            string targetLanguage,
                                            string voiceLanguage)
        {
            using (var audioInput = Helper.OpenStream(stream))
            {
                return await Translate(languageLocale, targetLanguage, voiceLanguage, audioInput);
            }
        }

        /// <summary>
        /// Method that converts an audio file to text
        /// </summary>
        /// <param name="language">
        /// Receives a Language locale as a parameter. <para/>
        /// Example: 'en-US' for english (United States), 'es-ES' for spanish (Spain) <para/>
        /// Use the LanguageLocale struct at the Services class to choose a proper language.<para/>
        /// </param>
        /// <param name="fileName">Filepath of a WAV File</param>
        /// <returns>A string containing the text obtained from the WAV file</returns>
        public async Task<string> GetTextFromSpeech(string language, string fileName)
        {
            using (var audioInput = Helper.OpenWavFile(fileName))
            {
                return await RecognizeText(language, audioInput);
            }
        }

        /// <summary>
        /// Method that converts an audio stream to text
        /// </summary>
        /// <param name="language">
        /// Receives a Language locale as a parameter. <para/>
        /// Example: 'en-US' for english (United States), 'es-ES' for spanish (Spain) <para/>
        /// Use the LanguageLocale struct at the Services class to choose a proper language.<para/>
        /// <param name="stream">A Stream containing audio</param>
        /// <returns>A string containing the text obtained from the stream</returns>
        public async Task<string> GetTextFromSpeech(string language, Stream stream)
        {
            using (var audioInput = Helper.OpenStream(stream))
            {
                return await RecognizeText(language, audioInput);
            }
        }

        private static async Task<string> RecognizeText(string language, AudioConfig audioInput)
        {
            var config = SpeechConfig.FromSubscription(cognitiveServicesSubscriptionKey, cognitiveServicesAzureRegion);
            config.SpeechRecognitionLanguage = language;

            string result = String.Empty;
            var stopRecognition = new TaskCompletionSource<int>();

            var recognizer = new SpeechRecognizer(config, audioInput);

            recognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                {
                    result = e.Result.Text;
                    Console.WriteLine(result);
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

        private static async Task<Translation> Translate(string languageLocale, string targetLanguage, string voiceLanguage, AudioConfig audioInput)
        {
            var translation = new Translation();

            var config = SpeechTranslationConfig.FromSubscription(cognitiveServicesSubscriptionKey, cognitiveServicesAzureRegion);

            var stopRecognition = new TaskCompletionSource<int>();

            config.SpeechRecognitionLanguage = languageLocale;
            config.AddTargetLanguage(targetLanguage);
            config.VoiceName = voiceLanguage;

            var recognizer = new TranslationRecognizer(config, audioInput);
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