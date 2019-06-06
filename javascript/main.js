(function() {
    "use strict";
    var sdk = require("microsoft-cognitiveservices-speech-sdk");
    var fs = require("fs");

    var settings = require("./settings");
    var speech = require("./speech");
    var translate = require("./translation");
    
    settings.filename = process.argv[2];
    settings.originLanguage = process.argv[3];
    settings.targetLanguage = process.argv[4];
    settings.targetLanguageTranslation = process.argv[5];

    var pushStream = sdk.AudioInputStream.createPushStream();
    fs.createReadStream(settings.filename).on('data', function(arrayBuffer){
      pushStream.write(arrayBuffer.buffer);
    }).on('end', function() {
      pushStream.close();
    });

    // Translation service also returns recognized text in original language
    console.log("Now translating from: " + settings.filename);
                translate.main(settings, pushStream);
    
    // Just for recognizing speech and not translating
    // console.log("Now recognizing speech from: " + settings.filename);
    //             speech.main(settings, pushStream);
  }());