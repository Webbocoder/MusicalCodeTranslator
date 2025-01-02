using MusicalCodeTranslator.Translation.TextToMusicalString;

namespace MusicalCodeTranslator.UserInteraction;

public class TranslatorConsoleUserInteraction : BasicConsoleUserInteraction, ITranslatorUserInteraction
{
    public TranslationDirection DetermineDirection()
    {
        ShowMessage("Would you like to encode [e] or decode [d]?");

        var validResponse = false;
        TranslationDirection direction = default;
        string userDecision;
        while (!validResponse)
        {
            userDecision = FetchUserInput();

            switch (userDecision.ToLower().Trim())
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
    public void PrintTranslation(string translation)
    {
        ShowMessage("Translation:");
        PrintEmptyLine();
        ShowMessage(translation);
        PrintEmptyLine();
    }
}