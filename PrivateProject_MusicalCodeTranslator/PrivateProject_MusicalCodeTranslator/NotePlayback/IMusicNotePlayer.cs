using PrivateProject_MusicalCodeTranslator.Models;

namespace PrivateProject_MusicalCodeTranslator.NotePlayback;

public interface IMusicNotePlayer
{
    void Play(MusicNote note);
    void Play(List<MusicNote> notes);
    void Play(List<MusicalWord> notes);
}
