using System;
using System.IO;
using _MyAssets.Scripts.Playground;
using MessagePack;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    public DataSerializer SaveData { get; private set; }
    public bool SaveFileExists { get; private set; }

    private string _saveFilePath;
    
    private void Awake()
    {
        Instance = this;
        _saveFilePath = Application.persistentDataPath + "/SaveData.dat";
    }

    private void Start()
    {
        SaveData = new DataSerializer();
    }

    [ContextMenu("Save")]
    public void SaveGame()
    {
        byte[] bytes = MessagePackSerializer.Serialize<DataSerializer>(SaveData);
        var json = MessagePackSerializer.ConvertToJson(bytes);
        var file = OpenSaveFile(false);
        file.Write(bytes);
        file.Close();
    }

    [ContextMenu("Load")]
    public void LoadGame()
    {
        var file = OpenSaveFile(true);
        if (file == null)
        {
            SaveFileExists = false;
            return;
        }
        SaveFileExists = true;
        byte[] bytes = new byte[file.Length];
        file.Read(bytes,0, (int)file.Length);
        SaveData = MessagePackSerializer.Deserialize<DataSerializer>(bytes);
        file.Close();
    }

    private FileStream OpenSaveFile(bool loading)
    {
        FileStream file = null;
        if (!File.Exists(_saveFilePath))
        {
            file = File.Create(_saveFilePath);
            file.Close();
            return null;
        }
        if(!loading) file = File.OpenWrite(_saveFilePath);
        if (loading)
        {
            file = File.OpenRead(_saveFilePath);
        }
        return file;
    }

    public void UpdateJellyAge(JellyStats.JellyAge age)
    {
        SaveData.JellyAge = age;
    }

    public void UpdateJellyHunger(float hunger)
    {
        SaveData.CurrentHunger = hunger;
    }

    public void UpdateMood(int mood)
    {
        SaveData.CurrentMood = mood;
    }

    public void UpdateSleepy(float sleepy)
    {
        SaveData.CurrentSleepy = sleepy;
    }

    public void UpdateLove(float love)
    {
        SaveData.Love = love;
    }

    public void SaveLeftItem(int itemID)
    {
        SaveData.LeftItemID = itemID;
    }

    public void SaveRightItem(int itemID)
    {
        SaveData.RightItemID = itemID;
    }

    public void SaveBerries(int berries)
    {
        SaveData.Berries = berries;
    }

    public void SaveJellyDew(int dew)
    {
        SaveData.JellyDew = dew;
    }

    public void SaveSelectedShell(int shell)
    {
        SaveData.SelectedShell = shell;
    }

    public void SaveEvolutionData(int evolutionDataID)
    {
        SaveData.SelectedEvolution = evolutionDataID;
    }

    public void SaveGardenData(int selectedBush, int unlockedPlants)
    {
        SaveData.UnlockedPlants = unlockedPlants;
        SaveData.SelectedBush = selectedBush;
    }
}