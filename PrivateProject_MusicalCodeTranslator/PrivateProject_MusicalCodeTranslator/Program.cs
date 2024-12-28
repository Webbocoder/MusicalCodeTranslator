var translateMe1 = "The fox jumped over the lazy dog.";
var translateMe2 = "abcdefghijklmnopqrstuvwxyz";
var translateMe3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
var translateMe4 = string.Join(" ", "abcdefghijklmnopqrstuvwxyz".ToCharArray());

string transation1 = Translator.Encode(translateMe1);
//string[] test = new[] { "a", "bb", " ", "," };
string transation2 = Translator.Encode(translateMe2);
string transation3 = Translator.Encode(translateMe3);
//Console.WriteLine(transation3.Length / 2);
string transation4 = Translator.Encode(translateMe4);

string decoded = Translator.Decode(transation1);

Console.ReadKey();

public static class Translator
{
    private static readonly char[] _lowercaseAlphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    private static readonly char[] _uppercaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private const int LengthOfMusicalAlphabet = 7;

    public static string Encode(string original)
    {
        return string.Join("", original.Select(character =>
        {
            if(!_lowercaseAlphabet.Contains(char.ToLower(character)))
            {
                return character.ToString();
            }

            char[] alphabet = char.IsLower(character) ? _lowercaseAlphabet : _uppercaseAlphabet;

            var index = Array.IndexOf(alphabet, character);
            var letter = alphabet[index % LengthOfMusicalAlphabet];
            var number = index / LengthOfMusicalAlphabet;

            return $"{letter}{number}";
        }));
    }

    public static string Decode(string original)
    {
        List<string> result = new List<string>();

        int counter = 0;
        while (counter <= original.Length)
        {
            var currentCharacter = original[counter];

            if(counter == original.Length - 1)
            {
                result.Add(currentCharacter.ToString());
                break;
            }

            var nextCharacter = original[counter + 1];

            if (char.IsLetter(currentCharacter) && char.IsDigit(nextCharacter))
            {
                char[] alphabet = char.IsLower(currentCharacter) ? _lowercaseAlphabet : _uppercaseAlphabet;
                int digit = (int)char.GetNumericValue(nextCharacter);
                int indexOfLetter = Array.IndexOf(alphabet, currentCharacter);
                var positionInAlphabet = (LengthOfMusicalAlphabet * digit) + indexOfLetter;

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