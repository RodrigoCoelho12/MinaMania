using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

partial class Player
{
    public TextMeshProUGUI extraLifeMessage;
    public GameObject extraLifeIndicator;

    public void ApplyExtraLife()
    {
        // Check if the player has extra lives to apply
        if (hasExtraLife)
        {
            Debug.Log($"Extra life applied!");
            healthBar.CurrentBarValue = healthBar.MaxBarValue;
            StartCoroutine(ShowExtraLifeMessage());
            extraLifeIndicator.SetActive(false);
            hasExtraLife = false;
        }
        else
        {
            Debug.Log("No extra lives to apply.");
            DeathRoutine();
        }
    }

    public IEnumerator ShowExtraLifeMessage()
    {
        extraLifeMessage.text = "Vida Extra aplicada!";
        yield return new WaitForSeconds(2f);
        extraLifeMessage.text = " ";
    }
}