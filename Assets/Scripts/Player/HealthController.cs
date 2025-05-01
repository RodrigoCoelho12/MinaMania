using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] float MaxHealth;
    [SerializeField] float CurrentHealth;
    [SerializeField] float RegenSpeed;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        
        
        if (CurrentHealth <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
            else
            {
                gameManager.GameOver();
            }
        }
    }










    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Enemy")!)
    //    {
    //        playerCurrentHealth -= enemyDamage * Time.deltaTime;
    //        playerHealtBar.fillAmount -= (enemyDamage * Time.deltaTime) / playerMaxHealth;
    //    }
    //}


    void Update()
    {
        //PlayerHealthRegen();

    }


    //public void PlayerHealthRegen()
    //{
    //    if (playerCurrentHealth < playerMaxHealth)
    //    {
    //        playerHealtBar.fillAmount = Mathf.MoveTowards(playerHealtBar.fillAmount, 1f, Time.deltaTime * playerRegenSpeed);
    //        playerCurrentHealth = Mathf.MoveTowards(playerCurrentHealth / playerMaxHealth, 1f, Time.deltaTime * playerRegenSpeed) * playerMaxHealth;
    //    }

    //    if (playerCurrentHealth <= 0)
    //    {
    //        Time.timeScale = 0;
    //        GameOverPanel.SetActive(true);
    //    }
    //}

}
