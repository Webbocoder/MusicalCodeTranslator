using PrivateProject_MusicalCodeTranslator.Models;

namespace PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote;

public interface IMusicalWordConstructor
{
    List<MusicalWord> TranslateToMusicalWords(int tempoInBPM, string musicallyEncodedString, string originalText, bool preservePunctuationInOriginal);
}
