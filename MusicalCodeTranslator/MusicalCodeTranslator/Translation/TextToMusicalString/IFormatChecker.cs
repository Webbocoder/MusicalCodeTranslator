namespace MusicalCodeTranslator.Translation.TextToMusicalString;

public interface IFormatChecker
{
    bool IsMusicallyEncodedString(string text);
}