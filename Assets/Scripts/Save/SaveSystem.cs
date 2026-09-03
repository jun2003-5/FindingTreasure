using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveSystem
{
    public static void Save(Player player)
    {
        //initialize and give some data
        GameData data = new GameData(player);
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("dataVersion", jsonData);
    }

    public static GameData Load()
    {
        string jsonData = PlayerPrefs.GetString("dataVersion");
        GameData data = JsonUtility.FromJson<GameData>(jsonData);

        if (data != null)
            return data;
        else
            return null;
    }
}
