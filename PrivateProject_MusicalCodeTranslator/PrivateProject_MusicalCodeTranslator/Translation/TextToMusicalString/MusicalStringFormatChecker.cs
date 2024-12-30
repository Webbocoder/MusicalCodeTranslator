using System.Text.RegularExpressions;

namespace PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;

public class MusicalStringFormatChecker : IFormatChecker
{
    public bool IsMusicallyEncodedString(string text)
    {
        // Remove punctuation and spaces.
        var stringWithNoSpacesNorPunctation = string.Join("", text.Where(char.IsLetterOrDigit));

        // Check that Length is even number. If not, return false. Check that each pair is a musicalLetter and digit.
        return stringWithNoSpacesNorPunctation.Length % 2 == 0 && Regex.IsMatch(stringWithNoSpacesNorPunctation, "^([A-Za-z][0-9])*$");
    }
}