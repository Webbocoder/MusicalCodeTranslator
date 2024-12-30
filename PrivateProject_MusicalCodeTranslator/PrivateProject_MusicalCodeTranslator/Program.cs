using PrivateProject_MusicalCodeTranslator.App;
using PrivateProject_MusicalCodeTranslator.UserInteraction;
using PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;
using PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote;
using PrivateProject_MusicalCodeTranslator.NotePlayback;

var musicalCodeTranslatorApp = new MusicalCodeTranslatorApp(
    new TranslatorConsoleUserInteraction(),
    new TextToMusicalStringEncoder(),
    new MusicalStringToMusicNoteTranslator(),
    new WindowsConsoleMusicNotePlayer(new BasicConsoleUserInteraction())
    );

musicalCodeTranslatorApp.Run();