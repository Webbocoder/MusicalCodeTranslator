using MusicalCodeTranslator.Models;

namespace MusicalCodeTranslator.NotePlayback;

public interface IMusicNotePlayer
{
    void Play(MusicNote note);
    void Play(List<MusicNote> notes);
    void PlayAndPrint(List<MusicalWord> notes);
}
