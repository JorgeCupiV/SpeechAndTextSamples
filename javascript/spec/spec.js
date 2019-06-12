var translate = require("./../translation");
var settings = require("./../settings");
var sdk = require("microsoft-cognitiveservices-speech-sdk");
var fs = require("fs");
  
describe("A suite", function() {
  it("contains spec with an expectation", function() {
    
    settings.filename = "audio-en-US-1.wav";
    settings.originLanguage = "en-US";
    settings.targetLanguage = "de-DE";
    settings.targetLanguageTranslation = "de";

    var pushStream = sdk.AudioInputStream.createPushStream();
    fs.createReadStream(settings.filename).on('data', function(arrayBuffer){
      pushStream.write(arrayBuffer.buffer);
    }).on('end', function() {
      pushStream.close();
    });
    translate.main(settings, pushStream);

    setTimeout(function(){
      console.log(settings.result);
    }, 5000);
    
    expect(true).toBe(true);
  });
});