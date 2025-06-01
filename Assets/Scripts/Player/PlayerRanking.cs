using TMPro;
using UnityEngine;

public class PlayerRanking : MonoBehaviour
{
    [Header("Player Stats")]
    private int points = 0;

    [Header("Canvas Elements")]
    public TextMeshProUGUI pointsText;

    private void Start()
    {
        if (pointsText == null)
            Debug.LogError("pointsText não atribuído no PlayerRanking.");

        UpdateInterface();
    }

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
