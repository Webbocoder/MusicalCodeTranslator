using PrivateProject_MusicalCodeTranslator.Models;

namespace PrivateProject_MusicalCodeTranslator.MusicalStringToMusicNote;

public class MusicalStringToMusicNoteTranslator : IMusicNoteConstructor
{
    private const double DefaultStartingNoteFrequencyInHertz = 110; // Second A below middle C.
    private const int LengthOfEnglishAlphabet = 26;
    private const int OneMinuteInMilliseconds = 60000;

    private static readonly int[] _positionsOfSemitonesInRange = new[] { 2, 5, 9, 12, 16, 19, 23 };
    // When comparing each pair of notesForMusicalWord in a 26-note range from the second A below middle C, above are the pairs which are a semitone apart.

    private static readonly char[] _lowercaseAlphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    private static readonly char[] _uppercaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private const int LengthOfMusicalAlphabet = 7;
    // The above three are also located in TextToMusicalStringEncoder. Is there any way to reduce this duplication?

    private readonly List<double> _frequencyCollection;

    public MusicalStringToMusicNoteTranslator()
    {
        _frequencyCollection = GenerateFrequencies();
    }

    public List<MusicalWord> TranslateToMusicalWords(int tempoInBPM, string musicallyEncodedString, string originalText, bool preservePunctuationInOriginal)
    {
        // Exclude punctuation (for now).
        var musicallyEncodedWords = TranslateToPunctuationlessArrayOfWords(musicallyEncodedString);
        var originalWords = preservePunctuationInOriginal ? originalText.Split(" ") : TranslateToPunctuationlessArrayOfWords(originalText);

        List<MusicalWord> musicalWords = new List<MusicalWord>();

        for (int i = 0; i < musicallyEncodedWords.Length; i++)
        {
            string word = musicallyEncodedWords[i];
            List<MusicNote> notesForMusicalWord = new List<MusicNote>();

            double duration = CalculateDuration(word, tempoInBPM);

            int counter = 0;
            while (counter < word.Length)
            {
                char letter = word[counter];
                int digit = (int)char.GetNumericValue(word[counter + 1]);
                double frequency = CalculateFrequency(letter, digit);

                notesForMusicalWord.Add(new MusicNote(frequency, duration));

                counter += 2;
            }

            var musicalWord = new MusicalWord(originalWords[i], word, notesForMusicalWord);
            musicalWords.Add(musicalWord);
        }

        return musicalWords;
    }

    private string[] TranslateToPunctuationlessArrayOfWords(string @string)
    {
        return string.Join("", @string.Where(character => char.IsLetterOrDigit(character) || character == ' ')).Split(" ");
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

        var noteFraction = 1.0; // 1.0 = crotchets/quarter-notesForMusicalWord; 1.0/2.0 = quaver/eighth-notesForMusicalWord; 1.0/4.0 = semiquaver/sixteenth-notesForMusicalWord ... 4.0 = semibreve/whole-notesForMusicalWord.
        double duration = OneMinuteInMilliseconds / (tempoInBPM * noteFraction) / (word.Length / 2);
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