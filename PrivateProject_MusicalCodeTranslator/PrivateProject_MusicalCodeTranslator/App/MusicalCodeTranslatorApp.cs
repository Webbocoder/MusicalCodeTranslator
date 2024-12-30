using PrivateProject_MusicalCodeTranslator.Model;
using PrivateProject_MusicalCodeTranslator.MusicalStringToMusicNote;
using PrivateProject_MusicalCodeTranslator.NotePlayback;
using PrivateProject_MusicalCodeTranslator.TextToMusicalStringTranslation;
using PrivateProject_MusicalCodeTranslator.UserInteraction;

namespace PrivateProject_MusicalCodeTranslator.App;

public class MusicalCodeTranslatorApp
{
    private readonly IUserInteration _userInteraction;
    private readonly IBiDirectionalTranslator _textualTranslator;
    private readonly IMusicNoteConstructor _musicNoteConstructor;
    private readonly IMusicNotePlayer _musicNotePlayer;
    private const int DefaultTempoInBPM = 60;

    public MusicalCodeTranslatorApp(
        IUserInteration userInteraction,
        IBiDirectionalTranslator textualTranslator,
        IMusicNoteConstructor musicNoteConstructor,
        IMusicNotePlayer musicNotePlayer)
    {
        _userInteraction = userInteraction;
        _textualTranslator = textualTranslator;
        _musicNoteConstructor = musicNoteConstructor;
        _musicNotePlayer = musicNotePlayer;
    }

    public void Run()
    {
        var continueIterating = true;
        while (continueIterating)
        {
            TranslationDirection direction = _userInteraction.DetermineDirection();

            _userInteraction.ShowMessage("Okay!");

            var userInput = _userInteraction.AskForTextToTranslate(direction);
            var translation = direction == TranslationDirection.Encode ? _textualTranslator.Encode(userInput) : _textualTranslator.Decode(userInput);

            _userInteraction.PrintTranslation(translation);

            if(direction == TranslationDirection.Encode)
            {
                var wantsToHear = _userInteraction.AskYesNoQuestion("Would you like to hear your creation?");

                if (wantsToHear)
                {
                    int tempoInBPM = DefaultTempoInBPM;
                    var usingDefaultTempo = _userInteraction.AskYesNoQuestion($"Would you like to use the default tempoInBPM of {DefaultTempoInBPM}bpm?");

                    if(!usingDefaultTempo)
                    {
                        tempoInBPM = _userInteraction.CollectInt("Please enter a tempoInBPM you would like: ");
                    }

                    List<MusicNote> notes = _musicNoteConstructor.GenerateNotes(tempoInBPM, translation);

                    bool wantsToHearAgain = true;
                    while(wantsToHearAgain)
                    {
                        _musicNotePlayer.Play(notes);

                        wantsToHearAgain = _userInteraction.AskYesNoQuestion("Would you like to hear that again?");
                    }
                    // Or could just do: _musicNoteConstructor.PlayEncodedString(tempoInBPM, translation);
                }
            }

            continueIterating = _userInteraction.AskYesNoQuestion("Would you like to encode/decode something else?");
        }

        _userInteraction.Exit();
    }

}