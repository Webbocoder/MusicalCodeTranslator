namespace MusicalCodeTranslator.Translation.TextToMusicalString.FormatChecker;

public interface IFormatChecker
{
    bool IsMusicallyEncodedCharacterPair(char leftCharacter, char rightCharacter);
    bool IsMusicallyEncodedString(string text);
}