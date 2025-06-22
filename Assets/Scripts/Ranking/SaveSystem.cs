using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string filePath => Application.persistentDataPath + "/playerData.json";

    public void SavePlayerDataList(RankingList rankingList)
    {
        string json = JsonUtility.ToJson(rankingList);
        File.WriteAllText(filePath, json);
    }
}
