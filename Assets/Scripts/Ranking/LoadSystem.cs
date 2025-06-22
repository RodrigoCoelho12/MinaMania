using System.IO;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    private string filePath => Application.persistentDataPath + "/playerData.json";
    public RankingList LoadRankingList()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<RankingList>(json);
        }
        return new RankingList();
    }
}
