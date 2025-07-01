using TMPro;
using UnityEngine;

public partial class Player
{
    [Header("Player Stats")]
    private float _score;
    public float score
    {
        get { return _score; }
        set { _score = value; }
    }

    public SaveSystem saveSystem;
    private PlayerData playerData;
    private RankingList rankingList;

    [Header("Canvas Elements")]
    public TextMeshProUGUI scoreText;

    public void IncreasePoints(int hordeCount)
    {
        Debug.Log("Entrei");
        score = score + (1000 * (1+(PlayerSO.Instance.hordeCount/10f)));
        UpdateScoreInterface();
        saveSystem.SaveRankingList(rankingList);
    }

    public void UpdateScoreInterface()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
        //Debug.Log(scoreText.text = "PONTOS: " + score.ToString());
    }
}
