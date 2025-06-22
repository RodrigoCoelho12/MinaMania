using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string filePath => Application.persistentDataPath + "/playerData.json";

    public void SavePlayerDataList(PlayerDataList dataList)
    {
        string json = JsonUtility.ToJson(dataList);
        File.WriteAllText(filePath, json);
    }

    public PlayerDataList LoadPlayerDataList()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerDataList>(json);
        }
        return new PlayerDataList();
    }
}
