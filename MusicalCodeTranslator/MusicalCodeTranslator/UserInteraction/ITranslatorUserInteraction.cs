using MusicalCodeTranslator.Translation.TextToMusicalString;

namespace MusicalCodeTranslator.UserInteraction;

public interface ITranslatorUserInteraction : IBasicUserInteraction
{
    TranslationDirection DetermineDirection();
    void PrintTranslation(string translation);
}