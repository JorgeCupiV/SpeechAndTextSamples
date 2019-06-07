# SpeechAndTextSamples

## DotNet Core
This is a small repo containing dotnetcore console apps samples on how to:
- Convert text to speech, 
- Convert speech to text
- Translation

Also, the javascript sample allows a wav file to be translated and get text extraction in its original language.

### Cognitive Services class library
The **Cognitive Services** class library contains a  **Services** class with a constructor that receives 3 parameteres from your Cognitive Services account:
- The name of the service
- The subscription key
- The region where the service was deployed

```csharp
cognitiveServices = new Services(resourceName, subscriptionKey,azureRegion);
```

Once the Service class its initialized, it's just a matter of using one of its three methods:
- GetSpeechFromText
- GetTextFromSpeech
- GetTranslationFromSpeech

#### GetSpeechFromText
Has two parameters: 
- **textToSpeech**: The text that will be converted to speech 
- **language**: A **String** that contains the target language for the audio translation. It has the a Full or Short service mapping format:
    - *"en-US-ZiraRUS"* for a female US english voice,
    - *"es-MX-Raul-Apollo"* for a male mexican voice,
    - *"zh-CN-HuihuiRUS"* for a female chinese mainland voice
    - *"fr-FR-Julie-Apollo"* for a female french voice
    - etc.

Returns a **Stream** that contains the audio obtained from the text.

```csharp
Stream dataStreamResult = await cognitiveServices.GetSpeechFromText(textToSpeech, language);

// Example
Stream dataStreamResult = await cognitiveServices.GetSpeechFromText("This is a sample string to be converted to speech in chinese", LanguageForSpeech.ChineseMainlandNeuralFemale);
```
#### GetTextFromSpeech
Has one parameter: 
- **fileName**: A **String** with the path of a wav file that contains audio to be extracted and converted to text

Returns a **String** that contains the text obtained from a wav file.
```csharp
string textFromSpeech = GetTextFromSpeech(fileName)

// Example
string textFromSpeech = GetTextFromSpeech("audio-en-US-1.wav")
```
#### GetTranslationFromSpeech
Has four parameters: 
- **fileName**: A **String** with the path of a wav file that contains audio to be extracted and converted to text
- **fromLanguageLocale**: A **String** that contains the origin language of the audio file.  It has the language locale format:
    - *"en-US"* for english from United States,
    - *"es-MX"* for spanish from Mexico,
    - *"zh-CN"* for chinese mainland
    - *"fr-FR"* for french from France
    - etc.
- **targetLanguage**: A **String** that contains the target language for the text translation. It has the language code format: 
    - *"en"* for english,
    - *"es"* for spanish,
    - *"zh-Hans"* for chinese traditional
    - *"fr"* for french
    - etc.
- **voiceLanguage**: A **String** that contains the target language for the audio translation. It has the a Full or Short service mapping format:
    - *"en-US-ZiraRUS"* for a female US english voice,
    - *"es-MX-Raul-Apollo"* for a male mexican voice,
    - *"zh-CN-HuihuiRUS"* for a female chinese mainland voice
    - *"fr-FR-Julie-Apollo"* for a female french voice
    - etc.
> For a full list of all the languages available, take a look at the [Language support page for Speech services.](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/language-support)

Returns a **Translation** object that contains the audio and text translated obtained from a wav file.
```csharp
Translation translation = GetTranslationFromSpeech(fileName, fromLanguageLocale, targetLanguage, voiceLanguage)

//Example
Translation translation = await GetTranslationFromSpeech(
                "audio-en-US-1.wav",
                LanguageLocale.EnglishUnitedStates,
                Language.Spanish,
                LanguageForSpeech.SpanishMexicoFemale)
```
## Javascript
To use the javascript sample, just run:

```console
node main.js {fileName} {localeLanguage for origin language} {localeLanguage for destiny language} {language for speech}
```

Examples:
```console
node main.js audio-en-US-1.wav en-US de-DE de

node main.js audio-zh-CN-1.wav zh-CN en-US en
```