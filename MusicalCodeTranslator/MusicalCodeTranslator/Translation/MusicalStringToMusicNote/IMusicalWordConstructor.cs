using MusicalCodeTranslator.Models;

namespace MusicalCodeTranslator.Translation.MusicalStringToMusicNote;

public interface IMusicalWordConstructor
{
    List<MusicalWord> TranslateToMusicalWords(int tempoInBPM, string musicallyEncodedString, string originalText, bool preservePunctuationInOriginal);
}
