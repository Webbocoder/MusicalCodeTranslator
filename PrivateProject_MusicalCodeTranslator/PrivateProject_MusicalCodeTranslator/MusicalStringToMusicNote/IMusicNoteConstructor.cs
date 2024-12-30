using PrivateProject_MusicalCodeTranslator.Models;

namespace PrivateProject_MusicalCodeTranslator.MusicalStringToMusicNote;

public interface IMusicNoteConstructor
{
    List<MusicalWord> TranslateToMusicalWords(int tempoInBPM, string musicallyEncodedString, string originalText, bool preservePunctuationInOriginal);
}
