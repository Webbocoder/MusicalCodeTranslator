namespace PrivateProject_MusicalCodeTranslator.Translation;

public static class TranslationDirectionExtensions
{
    public static string AsText(this TranslationDirection direction)
    {
        if (direction == TranslationDirection.Encode)
        {
            return "encode";
        }
        else if (direction == TranslationDirection.Decode)
        {
            return "decode";
        }

        return null;
    }
}