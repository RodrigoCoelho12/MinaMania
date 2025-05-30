using UnityEngine;

partial class Player
{
    [Header("Extra Life Properties")] // Number of extra lives to give
    private int currentLives; // Starting lives for the player //Fix: Set this to the desired starting lives

    public void ExtraLifeApplier()
    {
        // Check if the player has extra lives to apply
        if (currentLives == 0)
        {
            Debug.Log($"Extra life applied! Current lives: {currentLives}");
            healthBar.CurrentBarValue = healthBar.MaxBarValue;
            //Fix: Add extra lives to the player
        }
        else
        {
            Debug.Log("No extra lives to apply.");
            DeathRoutine();
        }
    }
}