using System.Collections.Generic;
using MessagePack;

[MessagePackObject]
public class DataSerializer
{

    public DataSerializer()
    {
        InitDefaultValues();
    }

    private void InitDefaultValues()
    {
        JellyAge = JellyStats.JellyAge.Baby;
        CurrentHunger = 0;
        CurrentMood = 0;
        CurrentSleepy = 0;
        Love = 0;
        LoveLevel = 0;
        LeftItemID = -1;
        RightItemID = -1;
        JellyDew = 0;
        Berries = 0;
        SelectedShell = 0;
        SelectedEvolution = 0;
        SelectedBush = 0;
        UnlockedPlants = 0;
        SoundLevel = 2;
        ShopItems = new List<int>(8)
        {
            0,0,0,0,0,0,0,0,
        };
    }
    
    //Jelly stats
    [Key(0)] public JellyStats.JellyAge JellyAge;
    [Key(1)] public float CurrentHunger;
    [Key(2)] public int CurrentMood;
    [Key(3)] public float CurrentSleepy;
    [Key(4)] public float Love;

    [Key(14)] public int LoveLevel;
    //Playground
    [Key(5)] public int LeftItemID;
    [Key(6)] public int RightItemID;
    //Currency
    [Key(7)] public int JellyDew;
    [Key(8)] public int Berries;
    //Visuals
    [Key(9)] public int SelectedShell;
    [Key(10)] public int SelectedEvolution;
    //garden
    [Key(11)] public int SelectedBush;
    [Key(12)] public int UnlockedPlants;
    //Shop
    [Key(13)] public List<int> ShopItems;

    [Key(14)] public int SoundLevel;
}
