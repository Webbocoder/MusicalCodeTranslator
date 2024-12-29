using PrivateProject_MusicalCodeTranslator.Translation;
using PrivateProject_MusicalCodeTranslator.UserInteraction;

namespace PrivateProject_MusicalCodeTranslator.App;

public class MusicalCodeTranslatorApp
{
    private readonly IUserInteration _userInteraction;
    private readonly IBiDirectionalTranslator _textualTranslator;

    public MusicalCodeTranslatorApp(IUserInteration userInteraction, IBiDirectionalTranslator textualTranslator)
    {
        _userInteraction = userInteraction;
        _textualTranslator = textualTranslator;
    }

    public void Run()
    {
        var continueIterating = true;
        while (continueIterating)
        {
            TranslationDirection direction = _userInteraction.DetermineDirection();

            _userInteraction.ShowMessage("Okay!");

            var userInput = _userInteraction.AskForTextToTranslate(direction);
            var translation = direction == TranslationDirection.Encode ? _textualTranslator.Encode(userInput) : _textualTranslator.Decode(userInput);

            _userInteraction.PrintTranslation(translation);

            continueIterating = _userInteraction.AskToStartAgain();
        }

        _userInteraction.Exit();
    }

}
