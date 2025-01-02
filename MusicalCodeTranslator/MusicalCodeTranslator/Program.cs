using MusicalCodeTranslator.App;
using MusicalCodeTranslator.UserInteraction;
using MusicalCodeTranslator.Translation.TextToMusicalString;
using MusicalCodeTranslator.Translation.MusicalStringToMusicNote;
using MusicalCodeTranslator.Translation.MusicalStringToMusicNote.FrequencyRangeGeneration;
using MusicalCodeTranslator.NotePlayback;

var translatorConsolUserInteraction = new TranslatorConsoleUserInteraction();

var musicalCodeTranslatorApp = new MusicalCodeTranslatorApp(
    translatorConsolUserInteraction,
    new TextToMusicalStringEncoder(),
    new MusicalStringFormatChecker(),
    new MusicalStringToMusicalWordTranslator(new FrequencyRangeGenerator()),
    new WindowsConsoleMusicNotePlayer(translatorConsolUserInteraction));

musicalCodeTranslatorApp.Run();

// Adding comment to test pushing from within submodule.