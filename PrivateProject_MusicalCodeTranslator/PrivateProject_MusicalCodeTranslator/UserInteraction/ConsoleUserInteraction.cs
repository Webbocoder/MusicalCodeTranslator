using PrivateProject_MusicalCodeTranslator.Translation;

namespace PrivateProject_MusicalCodeTranslator.UserInteraction;

public class ConsoleUserInteraction : IUserInteration
{
    private const string invalidResponseMessage = "Invalid response. Please try again.";

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public TranslationDirection DetermineDirection()
    {
        ShowMessage("Would you like to encode [e] or decode [d]?");

        var validResponse = false;
        TranslationDirection direction = default;
        string userDecision;
        while (!validResponse)
        {
            userDecision = Console.ReadLine();

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
                    ShowMessage(invalidResponseMessage);
                    break;
            }
        }

        Console.Clear();

        return direction;
    }

    public string AskForTextToTranslate(TranslationDirection direction)
    {
        Console.WriteLine($"Please enter some text you would like to {direction.AsText()}:");
        return Console.ReadLine();
    }
}

public interface IUserInteration
{
    string AskForTextToTranslate(TranslationDirection direction);
    TranslationDirection DetermineDirection();
    void ShowMessage(string message);
}