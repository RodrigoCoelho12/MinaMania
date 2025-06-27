using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Player player;

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
        rankingList = loadSystem.LoadRankingList();

        UpdateRankingUI();
    }

    public void InsertPlayerOnRanking()
    {
        string playerName = nameInputField.text;
        int score = player.score;

        PlayerData playerData = new PlayerData(playerName, score);

        rankingList.AddScore(playerData);
        saveSystem.SaveRankingList(rankingList);
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
