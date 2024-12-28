using PrivateProject_MusicalCodeTranslator.Translation;
using PrivateProject_MusicalCodeTranslator.UserInteraction;

namespace PrivateProject_MusicalCodeTranslator.App;

public class MusicalCodeTranslatorApp
{
    private readonly IUserInteration _userInteraction;

    public MusicalCodeTranslatorApp(IUserInteration userInteraction)
    {
        _userInteraction = userInteraction;
    }

    public void Run()
    {
        var continueIterating = true;
        string invalidResponseMessage = "Invalid response. Please try again.";

        while (continueIterating)
        {
            TranslationDirection direction = _userInteraction.DetermineDirection();

            _userInteraction.ShowMessage("Okay!");

            var userInput = _userInteraction.AskForTextToTranslate(direction);
            var translation = direction == TranslationDirection.Encode ? Translator.Encode(userInput) : Translator.Decode(userInput);

            Console.WriteLine("Translation:");
            Console.WriteLine();
            Console.WriteLine(translation);
            Console.WriteLine();
            Console.WriteLine("Would you like to try again?");

            validResponse = false;
            while (!validResponse)
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
    }

}
