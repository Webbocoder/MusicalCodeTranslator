﻿using PrivateProject_MusicalCodeTranslator.Models;
using PrivateProject_MusicalCodeTranslator.NotePlayback;
using PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote;
using PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;
using PrivateProject_MusicalCodeTranslator.UserInteraction;

namespace PrivateProject_MusicalCodeTranslator.App;

public class MusicalCodeTranslatorApp
{
    private readonly ITranslatorUserInteraction _userInteraction;
    private readonly IBiDirectionalTranslator _textualTranslator;
    private readonly IMusicNoteConstructor _musicalWordsConstructor;
    private readonly IMusicNotePlayer _musicNotePlayer;
    private const int DefaultTempoInBPM = 60;

    public MusicalCodeTranslatorApp(
        ITranslatorUserInteraction userInteraction,
        IBiDirectionalTranslator textualTranslator,
        IMusicNoteConstructor musicalWordsConstructor,
        IMusicNotePlayer musicNotePlayer)
    {
        _userInteraction = userInteraction;
        _textualTranslator = textualTranslator;
        _musicalWordsConstructor = musicalWordsConstructor;
        _musicNotePlayer = musicNotePlayer;
    }

    public void Run()
    {
        var continueIterating = true;
        while (continueIterating)
        {
            TranslationDirection direction = _userInteraction.DetermineDirection();

            _userInteraction.ShowMessage("Okay!");

            var stillEnteringTextToTranslate = false;
            string textToTranslate = default;
            string translation = default;

            do
            {
                textToTranslate = _userInteraction.CollectString($"Please enter some text you would like to {direction.AsText()}:");
                translation = direction == TranslationDirection.Encode ? _textualTranslator.Encode(textToTranslate) : _textualTranslator.Decode(textToTranslate);

                _userInteraction.PrintTranslation(translation);

                if
                (
                    direction == TranslationDirection.Decode
                    && !_textualTranslator.IsCorrectlyFormattedEncodedString(textToTranslate)
                    && _userInteraction.AskYesNoQuestion("That string might not have been a correctly-formatted musically encoded string. Would you like to enter it again?"))
                {
                    stillEnteringTextToTranslate = true;
                }

            } while (stillEnteringTextToTranslate);


            if(direction == TranslationDirection.Encode && _userInteraction.AskYesNoQuestion("Would you like to hear your creation?"))
            {
                int tempoInBPM = DefaultTempoInBPM;
                var usingDefaultTempo = _userInteraction.AskYesNoQuestion($"Would you like to use the default tempoInBPM of {DefaultTempoInBPM}bpm?");

                if(!usingDefaultTempo)
                {
                    tempoInBPM = _userInteraction.CollectInt("Please enter a tempoInBPM you would like: ");
                }

                var preservePunctuationInOriginal = _userInteraction.AskYesNoQuestion(@"Would you like to preserve the punctuation in the original text as the words appear on-screen?
Note: The playback ignores punctation for now.");

                List<MusicalWord> musicalWords = _musicalWordsConstructor.TranslateToMusicalWords(tempoInBPM, translation, textToTranslate, preservePunctuationInOriginal);

                bool wantsToHearAgain = true;
                while(wantsToHearAgain)
                {
                    _musicNotePlayer.PlayAndPrint(musicalWords);

                    wantsToHearAgain = _userInteraction.AskYesNoQuestion("Would you like to hear that again?");
                }
            }

            continueIterating = _userInteraction.AskYesNoQuestion("Would you like to encode/decode something else?");
        }

        _userInteraction.Exit("Musical Translator App");
    }

}