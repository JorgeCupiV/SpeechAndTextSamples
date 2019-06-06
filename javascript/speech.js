var sdk = require("microsoft-cognitiveservices-speech-sdk");

(function() {
"use strict";

module.exports = {
  main: function(settings, audioStream) {
    var audioConfig = sdk.AudioConfig.fromStreamInput(audioStream);
    var speechConfig = sdk.SpeechConfig.fromSubscription(settings.subscriptionKey, settings.serviceRegion);
    speechConfig.speechRecognitionLanguage = settings.originLanguage;

    var recognizer = new sdk.SpeechRecognizer(speechConfig, audioConfig);
    console.log("Starting recognition from "+ settings.filename);
    recognizer.recognizeOnceAsync(
      function (result) {
        console.log("Speech recognition service.\r\n Text recognized from speech: "+ result.privText);
        recognizer.close();
        recognizer = undefined;
      },
      function (err) {
        console.trace("err - " + err);
        recognizer.close();
        recognizer = undefined;
      }); 
  }  
}
}());