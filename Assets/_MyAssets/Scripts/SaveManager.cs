using System;
using System.IO;
using _MyAssets.Scripts.Playground;
using MessagePack;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public DataSerializer SaveData { get; private set; }

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
        Debug.Log("Saved: " + json);
    }

    [ContextMenu("Load")]
    public void LoadGame()
    {
        var file = OpenSaveFile(true);
        byte[] bytes = new byte[file.Length];
        file.Read(bytes,0, (int)file.Length);
        //var json = MessagePackSerializer.ConvertToJson(bytes);
        var loadedData = MessagePackSerializer.Deserialize<DataSerializer>(bytes);
        file.Close();
        //Debug.Log("Loaded: " + loadedData);
        //var json = MessagePackSerializer.ConvertToJson();
    }

    private FileStream OpenSaveFile(bool loading)
    {
        FileStream file = null;
        if (!File.Exists(_saveFilePath)) file = File.Create(_saveFilePath);
        if(!loading) file = File.OpenWrite(_saveFilePath);
        if (loading) file = File.OpenRead(_saveFilePath);
        return file;
    }
    
    private void SaveToFile()
    {
        FileStream file = File.Create(_saveFilePath); 
        /*SaveData data = new SaveData();
        data.savedInt = intToSave;
        data.savedFloat = floatToSave;
        data.savedBool = boolToSave;
        bf.Serialize(file, data);
        file.Close();*/
        Debug.Log("Game data saved!");
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
}
