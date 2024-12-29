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
                    ShowInvalidResponseMessage();
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

    public bool AskYesNoQuestion(string question)
    {
        ShowMessage(question);

        var validResponse = false;
        bool answer = default;

        while (!validResponse)
        {
            var userDecision = FetchUserInput();

            switch (userDecision.ToLower())
            {
                case "y":
                case "yes":
                    answer = true;
                    validResponse = true;
                    ClearConsole();
                    break;
                case "n":
                case "no":
                    answer = false;
                    validResponse = true;
                    ClearConsole();
                    break;
                default:
                    ShowInvalidResponseMessage();
                    break;
            }
        }
        return answer;
    }

    public void ShowInvalidResponseMessage()
    {
        PrintError(InvalidResponseMessage);
    }

    public void PrintError(string message)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        ShowMessage(message);
        Console.ForegroundColor = currentColor;
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

    public int CollectInt(string message)
    {
        ShowMessage(message);

        bool validResponse = false;
        int parsedInt;

        do
        {
            var userInput = FetchUserInput();
            validResponse = int.TryParse(userInput, out parsedInt);
            if(!validResponse)
            {
                ShowInvalidResponseMessage();
            }
        } while (!validResponse);

        return parsedInt;
    }
}