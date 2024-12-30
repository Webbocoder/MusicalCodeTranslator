using PrivateProject_MusicalCodeTranslator.Models;

namespace PrivateProject_MusicalCodeTranslator.Translation.MusicalStringToMusicNote;

public interface IMusicNoteConstructor
{
    List<MusicalWord> TranslateToMusicalWords(int tempoInBPM, string musicallyEncodedString, string originalText, bool preservePunctuationInOriginal);
}
