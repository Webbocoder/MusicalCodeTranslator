using MusicalCodeTranslator.Translation.TextToMusicalString.FormatChecker;

namespace MusicalCodeTranslator.Translation.TextToMusicalString;

public class TextToMusicalStringEncoder : IBiDirectionalTranslator
{
    private readonly IFormatChecker _formatChecker;

    public TextToMusicalStringEncoder(IFormatChecker formatChecker)
    {
        _formatChecker = formatChecker;
    }

    public string Encode(string original)
    {
        return string.Join("", original.Select(character =>
        {
            if (!char.IsLetter(character))
            {
                return character.ToString();
            }

            char[] alphabet = DecideAlphabet(character);

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
            char[] alphabet = DecideAlphabet(currentCharacter);
            char[] musicalAlphabet = alphabet.Take(7).ToArray();

            if (_formatChecker.IsMusicallyEncodedCharacterPair(currentCharacter, nextCharacter))
            {
                int digit = (int)char.GetNumericValue(nextCharacter);
                int positionInMusicalAlphabet = Array.IndexOf(musicalAlphabet, currentCharacter);
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

    private char[] DecideAlphabet(char character)
    {
        return char.IsLower(character) ? AlphabetHelpers.LowercaseEnglishAlphabet : AlphabetHelpers.UppercaseEnglishAlphabet;
    }
}