namespace MusicalCodeTranslator.UserInteraction;

public interface IBasicUserInteraction
{
    bool AskYesNoQuestion(string message);
    int CollectInt(string message);
    string CollectString(string message);
    void Exit(string appName);
    void PrintEmptyLine();
    void ShowMessage(string message);
}