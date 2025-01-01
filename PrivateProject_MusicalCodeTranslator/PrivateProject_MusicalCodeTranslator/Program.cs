﻿using PrivateProject_MusicalCodeTranslator.App;
using PrivateProject_MusicalCodeTranslator.UserInteraction;
using PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;
using PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote;
using PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote.FrequencyRangeGeneration;
using PrivateProject_MusicalCodeTranslator.NotePlayback;

var translatorConsolUserInteraction = new TranslatorConsoleUserInteraction();

var musicalCodeTranslatorApp = new MusicalCodeTranslatorApp(
    translatorConsolUserInteraction,
    new TextToMusicalStringEncoder(),
    new MusicalStringFormatChecker(),
    new MusicalStringToMusicalWordTranslator(new FrequencyRangeGenerator()),
    new WindowsConsoleMusicNotePlayer(translatorConsolUserInteraction));

musicalCodeTranslatorApp.Run();