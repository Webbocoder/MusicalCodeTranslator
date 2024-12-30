using PrivateProject_MusicalCodeTranslator.Model;

namespace PrivateProject_MusicalCodeTranslator.NotePlayback;

public interface IMusicNotePlayer
{
    void Play(MusicNote note);
    void Play(List<MusicNote> notes);
}
