using System.Text.RegularExpressions;

namespace PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;

public class TextToMusicalStringEncoder : IBiDirectionalTranslator
{
    public string Encode(string original)
    {
        return string.Join("", original.Select(character =>
        {
            if (!AlphabetHelpers.LowercaseEnglishAlphabet.Contains(char.ToLower(character)))
            {
                return character.ToString();
            }

            char[] alphabet = char.IsLower(character) ? AlphabetHelpers.LowercaseEnglishAlphabet : AlphabetHelpers.UppercaseEnglishAlphabet;

            var index = Array.IndexOf(alphabet, character);
            var musicalLetter = alphabet[index % AlphabetHelpers.LengthOfMusicalAlphabet];
            var number = index / AlphabetHelpers.LengthOfMusicalAlphabet;

            return $"{musicalLetter}{number}";
        }));
    }

    public string Decode(string original)
    {
        List<string> result = new List<string>();

        int counter = 0;
        while (counter < original.Length)
        {
            var currentCharacter = original[counter];

            if (counter == original.Length - 1)
            {
                result.Add(currentCharacter.ToString());
                break;
            }

            var nextCharacter = original[counter + 1];

            if (char.IsLetter(currentCharacter) && char.IsDigit(nextCharacter))
            {
                char[] alphabet = char.IsLower(currentCharacter) ? AlphabetHelpers.LowercaseEnglishAlphabet : AlphabetHelpers.UppercaseEnglishAlphabet;
                int digit = (int)char.GetNumericValue(nextCharacter);
                int positionInMusicalAlphabet = Array.IndexOf(alphabet, currentCharacter); // positionInMusicalAlphabet == positionWithDigitAccountedFor as the musicalLetter will only be in range A to G.
                var positionWithDigitAccountedFor = AlphabetHelpers.LengthOfMusicalAlphabet * digit + positionInMusicalAlphabet;

                result.Add(alphabet[positionWithDigitAccountedFor].ToString());

                counter += 2;
            }
            else
            {
                result.Add(currentCharacter.ToString());
                ++counter;
            }
        }

        return string.Join("", result);
    }

    public bool IsCorrectlyFormattedEncodedString(string text)
    {
        // Remove punctuation and spaces.
        var stringWithNoSpacesNorPunctation = string.Join("", text.Where(char.IsLetterOrDigit));

        // Check that Length is even number. If not, return false. Check that each pair is a musicalLetter and digit.
        return stringWithNoSpacesNorPunctation.Length % 2 == 0 && Regex.IsMatch(stringWithNoSpacesNorPunctation, "^([A-Za-z][0-9])*$");
    }
}