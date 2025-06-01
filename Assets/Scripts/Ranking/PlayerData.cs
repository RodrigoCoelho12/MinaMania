using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;

    public float Coin;

    public float RankFirstPlace;
    public float RankSecondPlace;
    public float RankThirdPlace;

    public bool conquista500Points;
    public bool conquista10000Points;
    public bool conquista50000Points;

    public bool conquista10Jumps;
    public bool conquista100Jumps;
    public bool conquista1000Jumps;
}