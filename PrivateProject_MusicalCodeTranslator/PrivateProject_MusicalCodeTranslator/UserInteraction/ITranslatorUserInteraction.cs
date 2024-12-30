using PrivateProject_MusicalCodeTranslator.Translation.TextToMusicalString;

namespace PrivateProject_MusicalCodeTranslator.UserInteraction;

public interface ITranslatorUserInteraction : IBasicUserInteraction
{
    TranslationDirection DetermineDirection();
    void PrintTranslation(string translation);
}