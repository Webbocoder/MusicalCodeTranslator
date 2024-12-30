using PrivateProject_MusicalCodeTranslator.Model;

namespace PrivateProject_MusicalCodeTranslator.MusicalStringToMusicNote;

public interface IMusicNoteConstructor
{
    List<MusicNote> GenerateNotes(int tempoInBPM, string translation);
}
