using PrivateProject_MusicalCodeTranslator.TextToMusicalStringTranslation;

namespace PrivateProject_MusicalCodeTranslator.UserInteraction;

public interface ITranslatorUserInteraction : IBasicUserInteraction
{
    TranslationDirection DetermineDirection();
    void PrintTranslation(string translation);
}