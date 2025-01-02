namespace MusicalCodeTranslator.Models;

public class MusicNote
{
    public double Frequency { get; init; }
    public double Duration { get; init; }

    public MusicNote(double frequency, double duration)
    {
        Frequency = frequency;
        Duration = duration;
    }

    public override string ToString() => $"{Frequency}:{Duration}";
}
