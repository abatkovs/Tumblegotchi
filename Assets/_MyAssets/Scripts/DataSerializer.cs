using _MyAssets.Scripts.Playground;
using MessagePack;

[MessagePackObject]
public class DataSerializer
{
    [Key(0)] public JellyStats.JellyAge JellyAge;
    [Key(1)] public float CurrentHunger;
    [Key(2)] public int CurrentMood;
    [Key(3)] public float CurrentSleepy;
    [Key(4)] public float Love;
    [Key(5)] public int LeftItemID;
    [Key(6)] public int RightItemID;
    [Key(7)] public int JellyDew;
    [Key(8)] public int Berries;
}
