namespace PrivateProject_MusicalCodeTranslator.UserInteraction;

public class BasicConsoleUserInteraction : IBasicUserInteraction
{
    private const string InvalidResponseMessage = "Invalid response. Please try again.";

    public string CollectString(string message)
    {
        ShowMessage(message);

        bool validResponse = false;
        string userInput = default;

        while (!validResponse)
        {
            userInput = FetchUserInput();

            if (string.IsNullOrEmpty(userInput))
            {
                ShowInvalidResponseMessage();
            }
            else
            {
                validResponse = true;
            }
        }

        return userInput;
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

    public string FetchUserInput()
    {
        return Console.ReadLine();
    }

    public void Exit(string appName)
    {
        ShowMessage($"Thank you for using the {appName}! Goodbye!");
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
            if (!validResponse)
            {
                ShowInvalidResponseMessage();
            }
        } while (!validResponse);

        return parsedInt;
    }
}
