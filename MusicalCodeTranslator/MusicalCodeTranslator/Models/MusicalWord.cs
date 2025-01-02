namespace MusicalCodeTranslator.Models;

public class MusicalWord
{
    public string Word { get; init; }
    public string Translation { get; init; }
    public List<MusicNote> Notes { get; init; }

    public MusicalWord(string word, string translation, List<MusicNote> notes)
    {
        Word = word;
        Translation = translation;
        Notes = notes;
    }

    public override string ToString() => $"\"{Word}\" / {Translation}";
}