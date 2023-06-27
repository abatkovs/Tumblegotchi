using System.Collections.Generic;
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
    [Key(9)] public int SelectedShell;
    [Key(10)] public int SelectedEvolution;
    //garden
    [Key(11)] public int SelectedBush;
    [Key(12)] public int UnlockedPlants;
    //Shop
    [Key(13)] public List<int> ShopItems;
    /*[Key(13)] public bool TetherBall;
    [Key(14)] public bool TireSwing;
    [Key(15)] public bool SwingSet;
    [Key(16)] public bool Slide;
    [Key(17)] public bool Ball;
    [Key(18)] public bool SeeSaw;
    [Key(19)] public bool MerryGoRound;
    [Key(20)] public int Sapling;*/
}
