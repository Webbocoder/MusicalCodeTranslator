using PrivateProject_MusicalCodeTranslator.Model;

namespace PrivateProject_MusicalCodeTranslator.NotePlayback;

public class WindowsConsoleMusicNotePlayer : IMusicNotePlayer
{
    public void Play(MusicNote note)
    {
        //Console.WriteLine(note);
        Console.Beep((int)note.Frequency, (int)note.Duration);
    }

    public void Play(List<MusicNote> notes)
    {
        foreach (MusicNote note in notes)
        {
            Play(note);
        }
    }
}