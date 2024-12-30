using PrivateProject_MusicalCodeTranslator.Model;

namespace PrivateProject_MusicalCodeTranslator.MusicalStringToMusicNote;

public class MusicalStringToMusicNoteTranslator : IMusicNoteConstructor
{
    private const double DefaultStartingNoteFrequencyInHertz = 110; // Second A below middle C.
    private const int LengthOfEnglishAlphabet = 26;
    private const int OneMinuteInMilliseconds = 60000;

    private static readonly int[] _positionsOfSemitonesInRange = new[] { 2, 5, 9, 12, 16, 19, 23 };
    // When comparing each pair of notes in a 26-note range from the second A below middle C, above are the pairs which are a semitone apart.

    private static readonly char[] _lowercaseAlphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    private static readonly char[] _uppercaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private const int LengthOfMusicalAlphabet = 7;
    // The above three are also located in TextToMusicalStringEncoder. Is there any way to reduce this duplication?

    private readonly List<double> _frequencyCollection;

    public MusicalStringToMusicNoteTranslator()
    {
        _frequencyCollection = GenerateFrequencies();
    }

    public List<MusicNote> GenerateNotes(int tempoInBPM, string translation)
    {
        // Exclude punctuation (for now).
        var alphanumericTranslation = string.Join("", translation.Where(character => char.IsLetterOrDigit(character) || character == ' '));
        var words = alphanumericTranslation.Split(" ");

        List<MusicNote> notes = new List<MusicNote>();

        foreach (var word in words)
        {
            double duration = CalculateDuration(word, tempoInBPM);

            int counter = 0;
            while (counter < word.Length)
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
        int indexOfFrequency = LengthOfMusicalAlphabet * digit + Array.IndexOf(alphabet, letter);
        return _frequencyCollection[indexOfFrequency];
    }

    private double CalculateDuration(string word, int tempoInBPM)
    {
        // Formula for converting tempoInBPM into durationInMilliseconds:
        // durationInMilliseconds = OneMinuteInMilliseconds / (tempoInBPM * noteFraction);

        var noteFraction = 1.0; // 1.0 = crotchets/quarter-notes; 1.0/2.0 = quaver/eighth-notes; 1.0/4.0 = semiquaver/sixteenth-notes ... 4.0 = semibreve/whole-notes.
        double duration = OneMinuteInMilliseconds / (tempoInBPM * noteFraction) / word.Length;
        return duration;
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
