(function() {
    "use strict";
    var sdk = require("microsoft-cognitiveservices-speech-sdk");
    var fs = require("fs");
    var subscriptionKey = "YourSubscriptionKey";
    var serviceRegion = "YourServiceRegion";
    var filename = "audio-zh-CN-1.wav";
    
    var pushStream = sdk.AudioInputStream.createPushStream();
    fs.createReadStream(filename).on('data', function(arrayBuffer){
      pushStream.write(arrayBuffer.buffer);
    }).on('end', function() {
      pushStream.close();
    });
    
    var audioConfig = sdk.AudioConfig.fromStreamInput(pushStream);
    var speechConfig = sdk.SpeechConfig.fromSubscription(subscriptionKey, serviceRegion);
    speechConfig.speechRecognitionLanguage = "zh-CN";
    
    var recognizer = new sdk.SpeechRecognizer(speechConfig, audioConfig);
    console.log("Starting recognition from "+ filename);
    recognizer.recognizeOnceAsync(
      function (result) {
        console.log("Text recognized: "+ result.privText);
    
        recognizer.close();
        recognizer = undefined;
      },
      function (err) {
        console.trace("err - " + err);
    
        recognizer.close();
        recognizer = undefined;
      });
  }());