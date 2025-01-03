using System.Text.RegularExpressions;

namespace MusicalCodeTranslator.Translation.TextToMusicalString.FormatChecker;

public class MusicalStringFormatChecker : IFormatChecker
{
    public bool IsMusicallyEncodedString(string text)
    {
        // Remove punctuation and spaces.
        var stringWithNoSpacesNorPunctation = string.Join("", text.Where(char.IsLetterOrDigit));

        // Check that Length is even number. If not, return false. Check that each pair is a musicalLetter and digit.
        if (!(stringWithNoSpacesNorPunctation.Length % 2 == 0) || !Regex.IsMatch(stringWithNoSpacesNorPunctation, "^([A-Ga-g][0-3])*$") || !Regex.IsMatch(text, "^([A-Ga-g][0-3])*$"))
            return false;

        return true;
    }

    public bool IsMusicallyEncodedCharacterPair(char leftCharacter, char rightCharacter)
    {
        return char.IsLetter(leftCharacter) &&
               (char.IsLower(leftCharacter) ? AlphabetHelpers.LowercaseEnglishAlphabet.Take(7).Contains(leftCharacter)
                                            : AlphabetHelpers.UppercaseEnglishAlphabet.Take(7).Contains(leftCharacter)) &&
               char.IsDigit(rightCharacter) &&
               rightCharacter >= '0' && rightCharacter <= '3';
    }
}