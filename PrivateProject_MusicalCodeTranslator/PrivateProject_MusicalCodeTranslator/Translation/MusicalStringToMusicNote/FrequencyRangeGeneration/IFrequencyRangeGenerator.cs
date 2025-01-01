namespace PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote.FrequencyRangeGeneration;

public interface IFrequencyRangeGenerator
{
    List<double> Generate(IEnumerable<int> semitonePairsForFirst8ve, double startingFrequency, int range);
}
