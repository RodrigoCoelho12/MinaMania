using TMPro;
using UnityEngine;

partial class Player
{
    public TextMeshProUGUI extraLifeMessage;
    public void ApplyExtraLife()
    {
        if (hasExtraLife)
        {
            // Check if the player has extra lives to apply
            if (hasExtraLife)
            {
                Debug.Log($"Extra life applied!");
                healthBar.CurrentBarValue = healthBar.MaxBarValue;
                extraLifeMessage.text = "Vida Extra aplicada!";
                hasExtraLife = false;
            }
            else
            {
                Debug.Log("No extra lives to apply.");
                DeathRoutine();
            }
        }
    }
}