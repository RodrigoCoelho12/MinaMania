using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingController : MonoBehaviour
{
    [Header("Ranking UI (Single Field)")]
    public List<TextMeshProUGUI> rankingTexts;

    private LoadSystem loadSystem;
    private PlayerData playerData;
    private RankingList rankingList;
    private SaveSystem saveSystem;

    private void Awake()
    {
        saveSystem = GetComponent<SaveSystem>();
        loadSystem = GetComponent<LoadSystem>();

        rankingList = new RankingList();
    }

    void Start()
    {
        InsertPlayerOnRanking("Amanda", 2500);
        InsertPlayerOnRanking("Dandan", 3200);
        InsertPlayerOnRanking("Duda", 3300);
        InsertPlayerOnRanking("Ivan", 1500);
        InsertPlayerOnRanking("Jaum", 2900);
        InsertPlayerOnRanking("Nelson", 1800);
        InsertPlayerOnRanking("Rodrigão", 1800);
        InsertPlayerOnRanking("Roque", 1800);

        saveSystem.SavePlayerDataList(rankingList);

        UpdateRankingUI();
    }

    public void InsertPlayerOnRanking(string name, int score)
    {
        PlayerData playerData = new PlayerData(name, score);

        rankingList.AddScore(playerData);
    }

    public void UpdateRankingUI()
    {
        List<PlayerData> top = rankingList.GetTopPlayers(rankingTexts.Count);
        for (int i = 0; i < rankingTexts.Count; i++)
        {
            if (i < top.Count)
                rankingTexts[i].text = $"{top[i].name} — {top[i].score}";
            else
                rankingTexts[i].text = "-";
        }
    }
}
