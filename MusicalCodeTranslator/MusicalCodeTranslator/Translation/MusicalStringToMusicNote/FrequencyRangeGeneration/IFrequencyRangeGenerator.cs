namespace MusicalCodeTranslator.Translation.MusicalStringToMusicNote.FrequencyRangeGeneration;

public interface IFrequencyRangeGenerator
{
    List<double> GenerateEqualTemperament(IEnumerable<int> semitonePairsForFirst8ve, double startingFrequency, int range);
}
