using PrivateProject_MusicalCodeTranslator.Models;
using PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote.FrequencyRangeGeneration;

namespace PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote;

public class MusicalStringToMusicalWordTranslator : IMusicalWordConstructor
{
    private const int OneMinuteInMilliseconds = 60000;
    private const double DefaultStartingNoteFrequencyInHertz = 110; // Second A below middle C.
    private readonly List<int> SemitonePairsForAToA = new List<int>() { 2, 5 }; // In one octave from A to A, semitones occur between the 2nd and 5th pairs of notes.

    private readonly IFrequencyRangeGenerator _frequencyRangeGenerator;

    public MusicalStringToMusicalWordTranslator(IFrequencyRangeGenerator frequencyRangeGenerator)
    {
        _frequencyRangeGenerator = frequencyRangeGenerator;
    }

    public List<MusicalWord> TranslateToMusicalWords(int tempoInBPM, string musicallyEncodedString, string originalText, bool preservePunctuationInOriginal)
    {
        var frequencyCollection = _frequencyRangeGenerator.Generate(
            SemitonePairsForAToA,
            DefaultStartingNoteFrequencyInHertz,
            AlphabetHelpers.LowercaseEnglishAlphabet.Length);

        // Exclude punctuation (for now).
        var musicallyEncodedWords = TranslateToPunctuationlessArrayOfWords(musicallyEncodedString);
        var originalWords = preservePunctuationInOriginal ? originalText.Split(" ") : TranslateToPunctuationlessArrayOfWords(originalText);

        List<MusicalWord> musicalWords = new List<MusicalWord>();

        for (int i = 0; i < musicallyEncodedWords.Length; i++)
        {
            string musicallyEncodedWord = musicallyEncodedWords[i];
            List<MusicNote> notesForMusicalWord = new List<MusicNote>();

            double duration = CalculateDuration(musicallyEncodedWord, tempoInBPM);

            int counter = 0;
            while (counter < musicallyEncodedWord.Length)
            {
                char letter = musicallyEncodedWord[counter];
                int digit = (int)char.GetNumericValue(musicallyEncodedWord[counter + 1]);
                double frequency = CalculateFrequency(letter, digit, frequencyCollection);

                notesForMusicalWord.Add(new MusicNote(frequency, duration));

                counter += 2;
            }

            var musicalWord = new MusicalWord(originalWords[i], musicallyEncodedWord, notesForMusicalWord);
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

    private double CalculateDuration(string musicallyEncodedWord, int tempoInBPM)
    {
        // Formula for converting tempoInBPM into durationInMilliseconds:
        // durationInMilliseconds = OneMinuteInMilliseconds / (tempoInBPM * noteFraction);

        // moteFraction:
        // 4.0 = semibreve / whole-note.
        // 2.0 = minim / half-note.
        // 1.0 = crotchets / quarter-note.
        // 1.0/2.0 = quaver / eighth-note.
        // 1.0/4.0 = semiquaver / sixteenth-note.

        var noteFraction = 1.0;
        var oneBeatInMilliseconds = OneMinuteInMilliseconds / (tempoInBPM * noteFraction);
        var lengthOfOriginalWord = musicallyEncodedWord.Length / 2;
        double duration = oneBeatInMilliseconds / lengthOfOriginalWord;
        return duration;
    }
}