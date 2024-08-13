using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System;

public class SaveLoadManager: MonoBehaviour
{
    public static event Action<PlayerLoadData> LoadPlayerDataAction;
    public void SavePlayerData(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        PlayerLoadData data = new PlayerLoadData(player);
        string playerDataPath = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(playerDataPath, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public void LoadPlayerData() 
    {
        string playerDataPath = Application.persistentDataPath + "/player.fun";
        if(File.Exists(playerDataPath))
        {
            FileStream stream = new FileStream(playerDataPath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerLoadData data = formatter.Deserialize(stream) as PlayerLoadData;
            stream.Close();
            LoadPlayerDataAction.Invoke(data);
            
        }
        else
        {
            Debug.LogError("file not found");
        }
    }    
}
