namespace PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;

public interface IBiDirectionalTranslator
{
    string Encode(string original);
    string Decode(string original);
    bool IsCorrectlyFormattedEncodedString(string textToTranslate);
}