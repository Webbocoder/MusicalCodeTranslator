using PrivateProject_MusicalCodeTranslator.Models;
using PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote.FrequencyRangeGeneration;

namespace PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote;

public class MusicalStringToMusicalWordTranslator : IMusicalWordConstructor
{
    private const double DefaultStartingNoteFrequencyInHertz = 110; // Second A below middle C.
    private const int OneMinuteInMilliseconds = 60000;
    private readonly List<int> SemitonPairsForAToA = new List<int>() { 2, 5 };

    private readonly IFrequencyRangeGenerator _frequencyRangeGenerator;

    public MusicalStringToMusicalWordTranslator(IFrequencyRangeGenerator frequencyRangeGenerator)
    {
        _frequencyRangeGenerator = frequencyRangeGenerator;
    }

    public List<MusicalWord> TranslateToMusicalWords(int tempoInBPM, string musicallyEncodedString, string originalText, bool preservePunctuationInOriginal)
    {
        var frequencyCollection = _frequencyRangeGenerator.Generate(
            SemitonPairsForAToA,
            DefaultStartingNoteFrequencyInHertz,
            AlphabetHelpers.LowercaseEnglishAlphabet.Length);

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
                double frequency = CalculateFrequency(letter, digit, frequencyCollection);

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

    private double CalculateFrequency(char letter, int digit, List<double> frequencyCollection)
    {
        var alphabet = char.IsLower(letter) ? AlphabetHelpers.LowercaseEnglishAlphabet : AlphabetHelpers.UppercaseEnglishAlphabet;
        int indexOfFrequency = AlphabetHelpers.LengthOfMusicalAlphabet * digit + Array.IndexOf(alphabet, letter);
        return frequencyCollection[indexOfFrequency];
    }

    private double CalculateDuration(string word, int tempoInBPM)
    {
        // Formula for converting tempoInBPM into durationInMilliseconds:
        // durationInMilliseconds = OneMinuteInMilliseconds / (tempoInBPM * noteFraction);

        var noteFraction = 1.0; // 1.0 = crotchets/quarter-notesForMusicalWord; 1.0/2.0 = quaver/eighth-notesForMusicalWord; 1.0/4.0 = semiquaver/sixteenth-notesForMusicalWord ... 4.0 = semibreve/whole-notesForMusicalWord.
        double duration = OneMinuteInMilliseconds / (tempoInBPM * noteFraction) / (word.Length / 2);
        return duration;
    }
}