using MusicalCodeTranslator.Models;
using MusicalCodeTranslator.NotePlayback;
using MusicalCodeTranslator.Translation.MusicalStringToMusicalWord;
using MusicalCodeTranslator.Translation.TextToMusicalString;
using MusicalCodeTranslator.Translation.TextToMusicalString.Direction;
using MusicalCodeTranslator.Translation.TextToMusicalString.FormatChecker;
using MusicalCodeTranslator.UserInteraction;

namespace MusicalCodeTranslator.App;

public class MusicalCodeTranslatorApp
{
    private readonly ITranslatorUserInteraction _userInteraction;
    private readonly IBiDirectionalTranslator _textualTranslator;
    private readonly IFormatChecker _formatChecker;
    private readonly IMusicalWordConstructor _musicalWordsConstructor;
    private readonly IMusicNotePlayer _musicNotePlayer;
    private const int DefaultTempoInBPM = 60;

    public MusicalCodeTranslatorApp(
        ITranslatorUserInteraction userInteraction,
        IBiDirectionalTranslator textualTranslator,
        IFormatChecker formatChecker,
        IMusicalWordConstructor musicalWordsConstructor,
        IMusicNotePlayer musicNotePlayer)
    {
        _userInteraction = userInteraction;
        _textualTranslator = textualTranslator;
        _formatChecker = formatChecker;
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
            bool containsSomethingToHear = default;

            do
            {
                textToTranslate = _userInteraction.CollectString($"Please enter some text you would like to {direction.AsText()}:").Trim();
                translation = direction == TranslationDirection.Encode
                    ? _textualTranslator.Encode(textToTranslate)
                    : _textualTranslator.Decode(textToTranslate);

                _userInteraction.PrintTranslation(translation);

                stillEnteringTextToTranslate = direction == TranslationDirection.Decode && !_formatChecker.IsMusicallyEncodedString(textToTranslate);

                if (stillEnteringTextToTranslate)
                {
                    stillEnteringTextToTranslate = _userInteraction.AskYesNoQuestion("That string might not have been a correctly-formatted musically encoded string. Would you like to enter it again?");
                }

            } while (stillEnteringTextToTranslate);

            if(direction == TranslationDirection.Encode && !textToTranslate.Any(char.IsLetter))
            {
                _userInteraction.ShowMessage("It looks like the text your entered contained just numbers and/or punctuation. If you enter at least one letter, you'll have the option to hear it!");
            }
            else if (direction == TranslationDirection.Encode && _userInteraction.AskYesNoQuestion("Would you like to hear your creation?"))
            {
                int tempoInBPM = DefaultTempoInBPM;
                var usingDefaultTempo = _userInteraction.AskYesNoQuestion($"Would you like to use the default tempoInBPM of {DefaultTempoInBPM}bpm?");

                if(!usingDefaultTempo)
                {
                    tempoInBPM = _userInteraction.CollectInt("Please enter a tempoInBPM you would like: ");
                }

                var preservePunctuationInOriginal = textToTranslate.Any(char.IsPunctuation) && _userInteraction.AskYesNoQuestion(@"Would you like to preserve the punctuation from the original text as the words appear on-screen?
Note: The playback currently ignores excess characters either way.");

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