using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static bool isLoaded;
    
    private static string fileEnd = "/player.cyka";
    public static string path = Application.persistentDataPath + fileEnd;

    public static void SavePlayer(ItemInventory player)
    {
        //Saves player data to the path created by Application.persistentDataPath
        //Also converts the file into binary
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        //Finds save path and loads file if possible and decodes file from binary
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;
            
        }
        else
        {
            Debug.LogError("Save file was not found in " + path);
            return null;
        }
    }

}
//Written by Ashton Westrick with assistance from https://www.youtube.com/watch?v=XOjd_qU2Ido
