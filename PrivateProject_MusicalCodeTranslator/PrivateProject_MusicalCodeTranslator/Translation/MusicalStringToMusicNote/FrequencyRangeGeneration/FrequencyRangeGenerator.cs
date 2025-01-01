namespace PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote.FrequencyRangeGeneration;

public class FrequencyRangeGenerator : IFrequencyRangeGenerator
{
    public List<double> Generate(IEnumerable<int> semitonePairsForFirst8ve, double startingFrequency, int range)
    {
        var frequencies = new List<double>() { startingFrequency };
        double twelfthRootOf2 = Math.Pow(2, 1.0 / 12); // For increasing by a semitone.
        double sixthRootOf2 = Math.Pow(2, 1.0 / 6); // For increasing by a whole tone.

        for (int i = 1; i < range; ++i)
        {
            if (semitonePairsForFirst8ve.Contains(i % AlphabetHelpers.LengthOfMusicalAlphabet))
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