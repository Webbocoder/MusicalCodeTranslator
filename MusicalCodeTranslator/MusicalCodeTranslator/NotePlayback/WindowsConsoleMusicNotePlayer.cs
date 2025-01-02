using MusicalCodeTranslator.Models;
using MusicalCodeTranslator.UserInteraction;

namespace MusicalCodeTranslator.NotePlayback;

public class WindowsConsoleMusicNotePlayer : IMusicNotePlayer
{
    private readonly IBasicUserInteraction _basicUserInteraction;

    public WindowsConsoleMusicNotePlayer(IBasicUserInteraction basicUserInteraction)
    {
        _basicUserInteraction = basicUserInteraction;
    }

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

    public void PlayAndPrint(List<MusicalWord> notes)
    {
        foreach(MusicalWord word in notes)
        {
            _basicUserInteraction.ShowMessage(word.ToString());
            Play(word.Notes);
        }
    }
}