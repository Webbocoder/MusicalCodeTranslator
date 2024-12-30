namespace PrivateProject_MusicalCodeTranslator.TextToMusicalStringTranslation;

public interface IBiDirectionalTranslator
{
    string Encode(string original);
    string Decode(string original);
}