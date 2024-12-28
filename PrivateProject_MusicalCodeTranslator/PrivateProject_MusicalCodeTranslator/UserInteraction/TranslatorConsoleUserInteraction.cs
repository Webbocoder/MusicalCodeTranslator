using PrivateProject_MusicalCodeTranslator.Translation;

namespace PrivateProject_MusicalCodeTranslator.UserInteraction;

public class TranslatorConsoleUserInteraction : IUserInteration
{
    private const string InvalidResponseMessage = "Invalid response. Please try again.";

    public TranslationDirection DetermineDirection()
    {
        ShowMessage("Would you like to encode [e] or decode [d]?");

        var validResponse = false;
        TranslationDirection direction = default;
        string userDecision;
        while (!validResponse)
        {
            userDecision = FetchUserInput();

            switch (userDecision.ToLower())
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
                    ShowMessage(InvalidResponseMessage);
                    break;
            }
        }

        ClearConsole();

        return direction;
    }

    public string AskForTextToTranslate(TranslationDirection direction)
    {
        ShowMessage($"Please enter some text you would like to {direction.AsText()}:");
        return FetchUserInput();
    }

    public void PrintTranslation(string translation)
    {
        ShowMessage("Translation:");
        PrintEmptyLine();
        ShowMessage(translation);
        PrintEmptyLine();
    }

    public bool AskToStartAgain()
    {
        ShowMessage("Would you like to try again?");

        var validResponse = false;
        bool continueIterating = default;

        while (!validResponse)
        {
            var userDecision = FetchUserInput();

            switch (userDecision.ToLower())
            {
                case "y":
                case "yes":
                    continueIterating = true;
                    validResponse = true;
                    ClearConsole();
                    break;
                case "n":
                case "no":
                    continueIterating = false;
                    validResponse = true;
                    ClearConsole();
                    break;
                default:
                    ShowMessage(InvalidResponseMessage);
                    break;
            }
        }
        return continueIterating;
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintEmptyLine()
    {
        Console.WriteLine();
    }

    public void ClearConsole()
    {
        Console.Clear();
    }

    private string FetchUserInput()
    {
        return Console.ReadLine();
    }

    public void Exit()
    {
        ShowMessage("Thank you for using this Translator App! Goodbye!");
        ShowMessage("Press any key to exit.");
        Console.ReadKey();
    }
}