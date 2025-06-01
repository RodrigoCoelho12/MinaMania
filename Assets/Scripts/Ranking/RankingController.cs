using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingController : MonoBehaviour
{
    public Text PrimeiroRankingScoreText;
    public Text SegundoRankingScoreText;
    public Text TerceiroRankingScoreText;
    
    private LoadSystem loadSystem;
    private PlayerData playerData;


    void Start()
    {
        loadSystem = new LoadSystem();
        playerData = loadSystem.LoadPlayerData();
    }

    public void AtualizaRanking()
    {
        PrimeiroRankingScoreText.text = playerData.RankFirstPlace.ToString();
        SegundoRankingScoreText.text = playerData.RankSecondPlace.ToString();
        TerceiroRankingScoreText.text = playerData.RankThirdPlace.ToString();
    }
}
