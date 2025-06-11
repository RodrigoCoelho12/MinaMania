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

    void Start()
    {
        loadSystem = new LoadSystem();
        playerData = loadSystem.LoadPlayerData();
        rankingList = new RankingList();

        rankingList.AddScore(2500, "Amanda");
        rankingList.AddScore(3200, "Dandan");
        rankingList.AddScore(3300, "Duda");
        rankingList.AddScore(1500, "Ivan");
        rankingList.AddScore(2900, "Jaum");
        rankingList.AddScore(1800, "Nelson");
        rankingList.AddScore(1800, "Rodrigão");
        rankingList.AddScore(1800, "Roque");

        UpdateRankingUI();
    }

    public void InsertPlayerOnRanking()
    {
        rankingList.AddScore(playerData.score, playerData.name);
        UpdateRankingUI();
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
