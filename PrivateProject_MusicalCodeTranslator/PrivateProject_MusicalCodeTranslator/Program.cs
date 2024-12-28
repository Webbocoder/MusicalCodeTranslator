// Testing...

//var translateMe1 = "The fox jumped over the lazy dog.";
//var translateMe2 = "abcdefghijklmnopqrstuvwxyz";
//var translateMe3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
//var translateMe4 = string.Join(" ", "abcdefghijklmnopqrstuvwxyz".ToCharArray());
//var translateMe5 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ     A";
//var translateMe6 = "The Quick Brown Fox Jumps Over The Lazy Dog!"; // Mixed case with punctuation
//var translateMe7 = "The quick brown fox 123 jumped over! @#$%^&*()"; // Numbers and special characters
//var translateMe8 = "   Hello, World!   "; // Leading and trailing spaces, punctuation
//var translateMe9 = "aaaaaaaaaaaaaaabbbbbbbbbb"; // Repeated characters to test large repetitions
//var translateMe10 = new string('a', 10000); // Very long string of 'a's to test large input handling
//var translateMe11 = "z" + new string('z', 100); // One character repeated to test large repetitions
//var translateMe12 = ""; // Edge case: Empty string

//string transation1 = Translator.Encode(translateMe1);
//string transation2 = Translator.Encode(translateMe2);
//string transation3 = Translator.Encode(translateMe3);
//string transation4 = Translator.Encode(translateMe4);
//string transation5 = Translator.Encode(translateMe5);
//string transation6 = Translator.Encode(translateMe6);
//string transation7 = Translator.Encode(translateMe7);
//string transation8 = Translator.Encode(translateMe8);
//string transation9 = Translator.Encode(translateMe9);
//string transation10 = Translator.Encode(translateMe10);
//string transation11 = Translator.Encode(translateMe11);
//string transation12 = Translator.Encode(translateMe12);

//string decoded1 = Translator.Decode(transation1);
//string decoded2 = Translator.Decode(transation2);
//string decoded3 = Translator.Decode(transation3);
//string decoded4 = Translator.Decode(transation4);
//string decoded5 = Translator.Decode(transation5);
//string decoded6 = Translator.Decode(transation6);
//string decoded7 = Translator.Decode(transation7);
//string decoded8 = Translator.Decode(transation8);
//string decoded9 = Translator.Decode(transation9);
//string decoded10 = Translator.Decode(transation10);
//string decoded11 = Translator.Decode(transation11);
//string decoded12 = Translator.Decode(transation12);

var continueIterating = true;
string invalidResponseMessage = "Invalid response. Please try again.";

while(continueIterating)
{
    Console.WriteLine("Would you like to encode [e] or decode [d]?");

    var validResponse = false;
    TranslationDirection direction = default;
    string userDecision;
    while(!validResponse)
    {
        userDecision = Console.ReadLine();

        switch(userDecision.ToLower())
        {
            case "e":
            case "encode":
                direction = TranslationDirection.Encode;
                validResponse = true;
                break;
            case "d":
            case "decode":
                direction = TranslationDirection.Decode;
                validResponse = true;
                break;
            default:
                Console.WriteLine(invalidResponseMessage);
                break;
        }
    }

    Console.Clear();

    Console.WriteLine($"Okay! Please enter some text you would like to {direction.AsText()}:");
    var userInput = Console.ReadLine();
    var translation = direction == TranslationDirection.Encode ? Translator.Encode(userInput) : Translator.Decode(userInput);

    Console.WriteLine("Translation:");
    Console.WriteLine();
    Console.WriteLine(translation);
    Console.WriteLine();
    Console.WriteLine("Would you like to try again?");

    validResponse = false;
    while(!validResponse)
    {
        userDecision = Console.ReadLine();

        switch (userDecision.ToLower())
        {
            case "y":
            case "yes":
                continueIterating = true;
                validResponse = true;
                Console.Clear();
                break;
            case "n":
            case "no":
                continueIterating = false;
                validResponse = true;
                Console.Clear();
                Console.WriteLine("Thank you for using the Musical Code Translator! Goodbye!");
                break;
            default:
                Console.WriteLine(invalidResponseMessage);
                break;
        }
    }

}

Console.WriteLine("Press any key to exit.");
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
        while (counter < original.Length)
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

public enum TranslationDirection
{
    Encode,
    Decode,
}

public static class TranslationDirectionExtensions
{
    public static string AsText(this TranslationDirection direction)
    {
        if(direction == TranslationDirection.Encode)
        {
            return "encode";
        }
        else if(direction == TranslationDirection.Decode)
        {
            return "decode";
        }

        return null;
    }
}