using TMPro;
using UnityEngine;

public partial class Player
{
    [Header("Player Stats")]
    private int _score;
    public int score
    {
        get { return _score; }
        set { _score = value; }
    }

    public SaveSystem saveSystem;
    private PlayerData playerData;
    private RankingList playerDataList;

    [Header("Canvas Elements")]
    public TextMeshProUGUI scoreText;

    public void IncreasePoints(int hordeCount)
    {
        Debug.Log("Entrei");
        score += 1000;
        //this.playerData.score = score;
        Debug.Log(score);
        UpdateInterface();
        saveSystem.SaveRankingList(playerDataList);
    }

    private void UpdateInterface()
    {
        if (scoreText != null)
            scoreText.text = "PONTOS: " + score.ToString();
        Debug.Log(scoreText.text = "PONTOS: " + score.ToString());
    }
}
