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
            var originalStream = File.OpenRead("audio-en-US-1.wav");

            // Act
            var result = await _services.GetSpeechFromText(textSample, language);
;
            // Assert
            Assert.Equal(result.Length,originalStream.Length);
        }

        [Fact]
        public async void GetTextFromSpeech()
        {
            // Arrange
            string textSample = "Hello I'm David can you hear me?";
            string fileName= "audio-en-US-1.wav";

            // Act
            var result = await _services.GetTextFromSpeech(fileName);

            // Assert
            Assert.Equal(textSample, result);
        }
    }
}