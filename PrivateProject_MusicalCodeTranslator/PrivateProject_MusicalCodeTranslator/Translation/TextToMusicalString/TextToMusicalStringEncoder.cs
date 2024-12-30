namespace PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;

public class TextToMusicalStringEncoder : IBiDirectionalTranslator
{
    public string Encode(string original)
    {
        return string.Join("", original.Select(character =>
        {
            if (!EnglishAndMusicalAlphabetHelpers.LowercaseAlphabet.Contains(char.ToLower(character)))
            {
                return character.ToString();
            }

            char[] alphabet = char.IsLower(character) ? EnglishAndMusicalAlphabetHelpers.LowercaseAlphabet : EnglishAndMusicalAlphabetHelpers.UppercaseAlphabet;

            var index = Array.IndexOf(alphabet, character);
            var letter = alphabet[index % EnglishAndMusicalAlphabetHelpers.LengthOfMusicalAlphabet];
            var number = index / EnglishAndMusicalAlphabetHelpers.LengthOfMusicalAlphabet;

            return $"{letter}{number}";
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
                char[] alphabet = char.IsLower(currentCharacter) ? EnglishAndMusicalAlphabetHelpers.LowercaseAlphabet : EnglishAndMusicalAlphabetHelpers.UppercaseAlphabet;
                int digit = (int)char.GetNumericValue(nextCharacter);
                int indexOfLetter = Array.IndexOf(alphabet, currentCharacter);
                var positionInAlphabet = EnglishAndMusicalAlphabetHelpers.LengthOfMusicalAlphabet * digit + indexOfLetter;

                result.Add(alphabet[positionInAlphabet].ToString());

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
}