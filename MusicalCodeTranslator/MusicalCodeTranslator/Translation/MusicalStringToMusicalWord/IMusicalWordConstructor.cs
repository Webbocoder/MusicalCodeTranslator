using MusicalCodeTranslator.Models;

namespace MusicalCodeTranslator.Translation.MusicalStringToMusicalWord;

public interface IMusicalWordConstructor
{
    List<MusicalWord> TranslateToMusicalWords(int tempoInBPM, string musicallyEncodedString, string originalText, bool preservePunctuationInOriginal);
}
