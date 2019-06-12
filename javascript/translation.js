var sdk = require("microsoft-cognitiveservices-speech-sdk");

(function() {
"use strict";

module.exports = {
  main: function(settings, audioStream) {
    var audioConfig = sdk.AudioConfig.fromStreamInput(audioStream);
    var translationConfig = sdk.SpeechTranslationConfig.fromSubscription(settings.subscriptionKey, settings.serviceRegion);
    translationConfig.speechRecognitionLanguage = settings.originLanguage;
    translationConfig.addTargetLanguage(settings.targetLanguage);
    var recognizer = new sdk.TranslationRecognizer(translationConfig, audioConfig);

    recognizer.recognized = function (s, e) {
      var str = "Text recognized: " + e.result.text + " \r\nTranslations:";
      var language = settings.targetLanguageTranslation;
      str += " [" + language + "] " + e.result.translations.get(language);
      str += "\r\n";
      settings.result = str;
    };
    
    recognizer.recognizeOnceAsync(
      function (result) {
        recognizer.close();
        recognizer = undefined;
      },
      function (err) {
        recognizer.close();
        console.log(err);
        recognizer = undefined;
      });
  }
}
}());