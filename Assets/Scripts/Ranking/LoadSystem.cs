using System.IO;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    private string filePath => Application.persistentDataPath + "/playerData.json";
    public RankingList LoadRankingList()
    {
        if (File.Exists(filePath))
        {
            Debug.Log(filePath);
            string json = File.ReadAllText(filePath);
            Debug.Log(json);

            RankingList list = JsonUtility.FromJson<RankingList>(json);
            if (list == null)
            {
                Debug.LogWarning("Falha ao desserializar o JSON. Criando nova lista de ranking.");
                list = new RankingList();
            }

            return list;
        }
        return new RankingList();
    }
}
