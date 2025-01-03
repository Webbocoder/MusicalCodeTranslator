using MusicalCodeTranslator.App;
using MusicalCodeTranslator.UserInteraction;
using MusicalCodeTranslator.Translation.TextToMusicalString;
using MusicalCodeTranslator.Translation.TextToMusicalString.FormatChecker;
using MusicalCodeTranslator.Translation.MusicalStringToMusicalWord;
using MusicalCodeTranslator.Translation.MusicalStringToMusicalWord.FrequencyRangeGeneration;
using MusicalCodeTranslator.NotePlayback;

var translatorConsolUserInteraction = new TranslatorConsoleUserInteraction();
var musicalStringFormatChecker = new MusicalStringFormatChecker();

Console.WriteLine(musicalStringFormatChecker.IsMusicallyEncodedString("!!!A1!!!")); //Should be True
Console.WriteLine(musicalStringFormatChecker.IsMusicallyEncodedString("A!!!1")); //Should be False
Console.WriteLine(musicalStringFormatChecker.IsMusicallyEncodedString("!!!HIJ!!!")); //Should be False
Console.WriteLine(musicalStringFormatChecker.IsMusicallyEncodedString("!!!123!!!")); //Should be False
Console.WriteLine(musicalStringFormatChecker.IsMusicallyEncodedString("R3")); //Should be False
Console.WriteLine(musicalStringFormatChecker.IsMusicallyEncodedString("A4")); //Should be False
Console.WriteLine(musicalStringFormatChecker.IsMusicallyEncodedString("R!3")); //Should be False
Console.WriteLine(musicalStringFormatChecker.IsMusicallyEncodedString("A!4")); //Should be False

var musicalCodeTranslatorApp = new MusicalCodeTranslatorApp(
    translatorConsolUserInteraction,
    new TextToMusicalStringEncoder(musicalStringFormatChecker),
    musicalStringFormatChecker,
    new MusicalStringToMusicalWordTranslator(new FrequencyRangeGenerator()),
    new WindowsConsoleMusicNotePlayer(translatorConsolUserInteraction));

musicalCodeTranslatorApp.Run();