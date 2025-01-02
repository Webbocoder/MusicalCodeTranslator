using MusicalCodeTranslator.Translation.TextToMusicalString.Direction;

namespace MusicalCodeTranslator.UserInteraction;

public interface ITranslatorUserInteraction : IBasicUserInteraction
{
    TranslationDirection DetermineDirection();
    void PrintTranslation(string translation);
}