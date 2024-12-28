using PrivateProject_MusicalCodeTranslator.Translation;
using PrivateProject_MusicalCodeTranslator.UserInteraction;

namespace PrivateProject_MusicalCodeTranslator.App;

public class TranslatorApp
{
    private readonly IUserInteration _userInteraction;
    private readonly ITranslator _translator;

    public TranslatorApp(IUserInteration userInteraction, ITranslator translator)
    {
        _userInteraction = userInteraction;
        _translator = translator;
    }

    public void Run()
    {
        var continueIterating = true;
        while (continueIterating)
        {
            TranslationDirection direction = _userInteraction.DetermineDirection();

            _userInteraction.ShowMessage("Okay!");

            var userInput = _userInteraction.AskForTextToTranslate(direction);
            var translation = direction == TranslationDirection.Encode ? _translator.Encode(userInput) : _translator.Decode(userInput);

            _userInteraction.PrintTranslation(translation);

            continueIterating = _userInteraction.AskToStartAgain();
        }

        _userInteraction.Exit();
    }

}
