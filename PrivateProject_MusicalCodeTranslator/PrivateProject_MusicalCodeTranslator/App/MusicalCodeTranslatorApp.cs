using PrivateProject_MusicalCodeTranslator.Translation;
using PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;
using PrivateProject_MusicalCodeTranslator.UserInteraction;

namespace PrivateProject_MusicalCodeTranslator.App;

public class MusicalCodeTranslatorApp
{
    private readonly IUserInteration _userInteraction;
    private readonly IBiDirectionalTranslator _textualTranslator;
    private readonly IAudioTranslator _audioTranslator;
    private const int DefaultTempoInBPM = 60;

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

                    List<MusicNote> notes = _audioTranslator.GenerateNotes(tempoInBPM, translation); // I believe the tempoInBPM will be needed for duration calculation.

                    bool wantsToHearAgain = true;
                    while(wantsToHearAgain)
                    {
                        _audioTranslator.PlayNotes(notes);

                        wantsToHearAgain = _userInteraction.AskYesNoQuestion("Would you like to hear that again?");
                    }
                    // Or could just do: _audioTranslator.PlayEncodedString(tempoInBPM, translation);
                }
            }

            continueIterating = _userInteraction.AskYesNoQuestion("Would you like to encode/decode something else?");
        }

        _userInteraction.Exit();
    }

}

public interface IAudioTranslator
{
    List<MusicNote> GenerateNotes(int tempoInBPM, string translation);
    void PlayNotes(List<MusicNote> notes);
}

public class MusicNote
{
    public double Frequency { get; init; }
    public double Duration { get; init; }

    public MusicNote(double frequency, double duration)
    {
        Frequency = frequency;
        Duration = duration;
    }

    public override string ToString() => $"{Frequency}:{Duration}";
}

public class MusicNoteToAudioTranslator : IAudioTranslator
{
    // Might break SRP(?) as not only plays the notes, but generates a frequency range too. Consider moving that logic into seperate class, or indeed write out the collection of frequencies manually.
    // Maybe this class can be MusicallyEncodedStringToFrequencyTranslator (or just MusicNoteConstructor (but it specifically translates from musically encoded strings)) then we can plug a _notePlayer (WindowsConsoleMusicNotePlayer) directly into MusicalCodeTranslatorApp and get rid of the PlayNotes method from this class and add it to IMusicNotePlayer and WindowsConsoleMusicNotePlayer.

    private readonly IMusicNotePlayer _musicNotePlayer;
    private const double DefaultStartingNoteFrequencyInHertz = 110; // Second A below middle C.
    private const int LengthOfEnglishAlphabet = 26;
    private static readonly int[] _positionsOfSemitonesInRange = new[] { 2, 5, 9, 12, 16, 19, 23 };
    // When comparing each pair of notes in a 26-note range from the second A below middle C, above are the pairs which are a semitone apart.
    private readonly List<double> _frequencyCollection;

    private static readonly char[] _lowercaseAlphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    private static readonly char[] _uppercaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private const int LengthOfMusicalAlphabet = 7;
    // The above three are also located in TextToMusicalStringEncoder. Is there any way to reduce this duplication?

    private const int OneMinuteInMilliseconds = 60000;

    public MusicNoteToAudioTranslator(IMusicNotePlayer musicNotePlayer)
    {
        _musicNotePlayer = musicNotePlayer;
        _frequencyCollection = GenerateFrequencies();
    }

    public List<MusicNote> GenerateNotes(int tempoInBPM, string translation)
    {
        // Exclude punctuation (for now).
        var alphanumericTranslation = string.Join("", translation.Where(character => char.IsLetterOrDigit(character) || character == ' '));
        var words = alphanumericTranslation.Split(" ");

        List<MusicNote> notes = new List<MusicNote>();

        foreach(var word in words)
        {
            double duration = CalculateDuration(word, tempoInBPM);

            int counter = 0;
            while(counter < word.Length)
            {
                char letter = word[counter];
                int digit = (int)char.GetNumericValue(word[counter + 1]);
                double frequency = CalculateFrequency(letter, digit);

                notes.Add(new MusicNote(frequency, duration));

                counter += 2;
            }
        }

        return notes;
    }

    private double CalculateFrequency(char letter, int digit)
    {
        var alphabet = char.IsLower(letter) ? _lowercaseAlphabet : _uppercaseAlphabet;
        int indexOfFrequency = (LengthOfMusicalAlphabet * digit) + Array.IndexOf(alphabet, letter);
        return _frequencyCollection[indexOfFrequency];
    }

    private double CalculateDuration(string word, int tempoInBPM)
    {
        // Formula for converting tempoInBPM into durationInMilliseconds:
        // durationInMilliseconds = OneMinuteInMilliseconds / (tempoInBPM * noteFraction);

        var noteFraction = 1.0; // 1.0 = crotchets/quarter-notes; 1.0/2.0 = quaver/eighth-notes; 1.0/4.0 = semiquaver/sixteenth-notes ... 4.0 = semibreve/whole-notes.
        double duration = (OneMinuteInMilliseconds / (tempoInBPM * noteFraction)) / word.Length;
        return duration;
    }

    public void PlayNotes(List<MusicNote> notes)
    {
        foreach (MusicNote note in notes)
        {
            _musicNotePlayer.PlayNote(note);
        }
    }

    private List<double> GenerateFrequencies()
    {
        var frequencies = new List<double>() { DefaultStartingNoteFrequencyInHertz };
        double twelfthRootOf2 = Math.Pow(2, 1.0 / 12); // For increasing by a semitone.
        double sixthRootOf2 = Math.Pow(2, 1.0 / 6); // For increasing by a whole tone.

        for (int i = 1; i < LengthOfEnglishAlphabet; ++i)
        {
            if (_positionsOfSemitonesInRange.Contains(i))
            {
                frequencies.Add(frequencies[i - 1] * twelfthRootOf2);
            }
            else
            {
                frequencies.Add(frequencies[i - 1] * sixthRootOf2);
            }
        }

        return frequencies;
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
        Console.WriteLine(note);
        Console.Beep((int)note.Frequency, (int)note.Duration);
    }
}