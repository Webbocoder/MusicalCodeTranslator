namespace PrivateProject_MusicalCodeTranslator.Translation;

public interface IBiDirectionalTranslator
{
    string Encode(string original);
    string Decode(string original);
}