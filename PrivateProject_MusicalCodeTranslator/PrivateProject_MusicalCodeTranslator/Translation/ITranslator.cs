namespace PrivateProject_MusicalCodeTranslator.Translation;

public interface ITranslator
{
    string Encode(string original);
    string Decode(string original);
}