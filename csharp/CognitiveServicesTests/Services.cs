using CognitiveServices;
using System.IO;
using Xunit;
using static CognitiveServices.Services;

namespace CognitiveServicesTests
{
    public class ServicesTests
    {
        static string subscriptionKey = "<YourCognitiveServiceSubscriptionKey>";
        static string resourceName = "<YourCognitiveServiceName>";
        static string azureRegion = AzureRegions.EastUs;

        private readonly Services _services;

        public ServicesTests()
        {
            _services = new Services(resourceName,subscriptionKey,azureRegion);
        }

        [Fact]
        public async void GetSpeechFromText()
        {
            // Arrange
            string textSample = "Hello, I'm David, can you hear me?";
            string language = LanguageForSpeech.EnglishNeuralFemale;
            string fileName = "audio-en-US-1.wav";
            Stream result;
            // Act
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                result = await _services.GetSpeechFromText(textSample, language);

            // Assert
                Assert.Equal(result.Length, stream.Length);
            }
        }

        [Fact]
        public async void GetTextFromSpeechFile()
        {
            // Arrange
            string textSample = "Hello I'm David can you hear me?";
            string fileName= "audio-en-US-1.wav";

            // Act
            var result = await _services.GetTextFromSpeech(Services.LanguageLocale.EnglishUnitedStates, fileName);

            // Assert
            Assert.Equal(textSample, result);
        }

        [Fact]
        public async void GetTextFromSpeechStream()
        {
            // Arrange
            string textSample = "Hello I'm David can you hear me?";
            string fileName = "audio-en-US-1.wav";
            string result = string.Empty;
            
            // Act
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                result = await _services.GetTextFromSpeech(Services.LanguageLocale.EnglishUnitedStates, stream);
            }
                
            // Assert
            Assert.Equal(textSample, result);
        }

        [Fact]
        public async void GetTranslationFromSpeechFile()
        {
            // Arrange
            string textSample = "Hola. Yo soy David. ¿Me puedes escuchar?";
            string fileName = "audio-en-US-1.wav";

            // Act
            var translation = await _services.GetTranslationFromSpeech(
                fileName,
                LanguageLocale.EnglishUnitedStates,
                Language.Spanish,
                LanguageForSpeech.SpanishMexicoFemale);

            // Assert
            Assert.Equal(translation.text, textSample);
        }

        [Fact]
        public async void GetTranslationFromSpeechStream()
        {
            // Arrange
            string textSample = "Hola. Yo soy David. ¿Me puedes escuchar?";
            string fileName = "audio-en-US-1.wav";
            var translation = new Translation();

            // Act
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                translation = await _services.GetTranslationFromSpeech(
                stream,
                LanguageLocale.EnglishUnitedStates,
                Language.Spanish,
                LanguageForSpeech.SpanishMexicoFemale);
            }
            // Assert
            Assert.Equal(translation.text, textSample);
        }
    }
}