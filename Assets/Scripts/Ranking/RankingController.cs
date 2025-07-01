using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public GameObject msgErroMaxima;
    public GameObject msgErroMinima;
    public Player player;

    [Header("Ranking UI (Single Field)")]
    public List<TextMeshProUGUI> rankingNameTexts;
    public List<TextMeshProUGUI> rankingScoreTexts;

    private LoadSystem loadSystem;
    private PlayerData playerData;
    private RankingList rankingList;
    private SaveSystem saveSystem;

    private void Awake()
    {
        saveSystem = GetComponent<SaveSystem>();
        loadSystem = GetComponent<LoadSystem>();

        rankingList = loadSystem.LoadRankingList();
        Debug.Log(rankingList);
    }

    void Start()
    {
        UpdateRankingUI();
    }
    public void InsertPlayerOnRanking()
    {
        string playerName = nameInputField.text;
        float score = player.score;

        if (playerName.Length > 8)
        {
            msgErroMaxima.SetActive(true);
            msgErroMinima.SetActive(false);
        }
        else if (playerName.Length < 3)
        {
            msgErroMinima.SetActive(true);
            msgErroMaxima.SetActive(false);
        }
        else
        {
            PlayerData playerData = new PlayerData(playerName, (int)score);

            rankingList.AddScore(playerData);
            saveSystem.SaveRankingList(rankingList);
            SceneManager.LoadScene(0);
        }
    }

    public void UpdateRankingUI()
    {
        List<PlayerData> top = rankingList.GetTopPlayers(rankingNameTexts.Count);

        for (int i = 0; i < rankingNameTexts.Count; i++)
        {
            if (i < top.Count)
            {
                rankingNameTexts[i].text = $"{top[i].name}";
                rankingScoreTexts[i].text = $"{top[i].score}";

            }
            else
            {
                rankingNameTexts[i].text = "-";
                rankingScoreTexts[i].text = "-";
            }
        }
    }
}

