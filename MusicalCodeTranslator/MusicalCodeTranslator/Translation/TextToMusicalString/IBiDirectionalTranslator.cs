namespace MusicalCodeTranslator.Translation.TextToMusicalString;

public interface IBiDirectionalTranslator
{
    string Encode(string original);
    string Decode(string original);
}