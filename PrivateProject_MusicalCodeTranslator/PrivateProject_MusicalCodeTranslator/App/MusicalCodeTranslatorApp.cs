using PrivateProject_MusicalCodeTranslator.Translation;
using PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;
using PrivateProject_MusicalCodeTranslator.UserInteraction;

namespace PrivateProject_MusicalCodeTranslator.App;

public class MusicalCodeTranslatorApp
{
    private readonly IUserInteration _userInteraction;
    private readonly IBiDirectionalTranslator _textualTranslator;
    private readonly IAudioTranslator _audioTranslator;

    public MusicalCodeTranslatorApp(IUserInteration userInteraction, IBiDirectionalTranslator textualTranslator, IAudioTranslator audioTranslator)
    {
        _userInteraction = userInteraction;
        _textualTranslator = textualTranslator;
        _audioTranslator = audioTranslator;
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

            var wantsToHear = _userInteraction.AskYesNoQuestion("Would you like to hear your creation?");

            if (wantsToHear)
            {
                List<MusicNote> notes = _audioTranslator.GenerateNotes(translation);
                _audioTranslator.PlayNotes(notes);
                // Or could just do: _audioTranslator.PlayEncodedString(translation);
            }

            continueIterating = _userInteraction.AskYesNoQuestion("Would you like to try again?");
        }

        _userInteraction.Exit();
    }

}

public interface IAudioTranslator
{
    List<MusicNote> GenerateNotes(string translation);
    void PlayNotes(List<MusicNote> notes);
}

public class MusicNote
{
    public int Frequency { get; init; }
    public int Duration { get; init; }

    public MusicNote(int frequency, int duration)
    {
        Frequency = frequency;
        Duration = duration;
    }
}

public class MusicNoteToAudioTranslator : IAudioTranslator
{
    private readonly IMusicNotePlayer _musicNotePlayer;

    public MusicNoteToAudioTranslator(IMusicNotePlayer musicNotePlayer)
    {
        _musicNotePlayer = musicNotePlayer;
    }

    public List<MusicNote> GenerateNotes(string translation)
    {
        throw new NotImplementedException();
    }

    public void PlayNotes(List<MusicNote> notes)
    {
        foreach (MusicNote note in notes)
        {
            _musicNotePlayer.PlayNote(note);
        }
    }
}

public interface IMusicNotePlayer
{
    void PlayNote(MusicNote note);
}

public class WindowsConsoleMusicNotePlayer : IMusicNotePlayer
{
    public void PlayNote(MusicNote note)
    {
        Console.Beep(note.Frequency, note.Duration);
    }
}