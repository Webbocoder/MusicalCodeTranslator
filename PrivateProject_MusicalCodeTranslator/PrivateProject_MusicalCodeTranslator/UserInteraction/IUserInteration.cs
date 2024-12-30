using PrivateProject_MusicalCodeTranslator.TextToMusicalStringTranslation;

namespace PrivateProject_MusicalCodeTranslator.UserInteraction;

public interface IUserInteration
{
    string AskForTextToTranslate(TranslationDirection direction);
    bool AskYesNoQuestion(string message);
    int CollectInt(string message);
    TranslationDirection DetermineDirection();
    void Exit();
    void PrintEmptyLine();
    void PrintTranslation(string translation);
    void ShowMessage(string message);
}