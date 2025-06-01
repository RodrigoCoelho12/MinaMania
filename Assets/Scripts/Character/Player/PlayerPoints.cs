using TMPro;
using UnityEngine;

public partial class Player
{
    [Header("Player Stats")]
    private int points = 0;

    [Header("Canvas Elements")]
    public TextMeshProUGUI pointsText;

    public void IncreasePoints()
    {
        points++;
        UpdateInterface();
    }

    private void UpdateInterface()
    {
        if (pointsText != null)
            pointsText.text = points.ToString();
    }
}
