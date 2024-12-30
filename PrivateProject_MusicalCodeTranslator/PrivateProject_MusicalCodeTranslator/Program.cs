using PrivateProject_MusicalCodeTranslator.App;
using PrivateProject_MusicalCodeTranslator.UserInteraction;
using PrivateProject_MusicalCodeTranslator.TextToMusicalStringTranslation;
using PrivateProject_MusicalCodeTranslator.MusicalStringToMusicNote;
using PrivateProject_MusicalCodeTranslator.NotePlayback;

var musicalCodeTranslatorApp = new MusicalCodeTranslatorApp(
    new TranslatorConsoleUserInteraction(),
    new TextToMusicalStringEncoder(),
    new MusicalStringToMusicNoteTranslator(),
    new WindowsConsoleMusicNotePlayer()
    );

musicalCodeTranslatorApp.Run();