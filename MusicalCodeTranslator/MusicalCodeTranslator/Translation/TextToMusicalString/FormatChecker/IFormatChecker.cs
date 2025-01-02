namespace MusicalCodeTranslator.Translation.TextToMusicalString.FormatChecker;

public interface IFormatChecker
{
    bool IsMusicallyEncodedString(string text);
}