using TMPro;
using UnityEngine;

public partial class Player
{
    [Header("Player Stats")]
    private int points = 0;

    [Header("Canvas Elements")]
    public TextMeshProUGUI pointsText;

    public void IncreasePoints(int hordeCount)
    {
        if(hordeCount > 1)
        {
            points += 1000;
            UpdateInterface();
        }
    }

    private void UpdateInterface()
    {
        if (pointsText != null)
            pointsText.text = "PONTOS: "+points.ToString();
    }
}
