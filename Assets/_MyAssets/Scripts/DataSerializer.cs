using MessagePack;

[MessagePackObject]
public class DataSerializer
{
    [Key(0)] public JellyStats.JellyAge JellyAge;
    [Key(1)] public float CurrentHunger;
    [Key(2)] public int CurrentMood;
    [Key(3)] public float CurrentSleepy;
    [Key(4)] public float Love;
}
