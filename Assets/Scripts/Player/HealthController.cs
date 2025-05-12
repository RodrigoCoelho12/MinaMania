using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] float regenSpeed;

    [SerializeField] Image playerHealthBar;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            maxHealth = 100f;
        }
        
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        
        
        if (currentHealth <= 0)
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


    private void OnTriggerEnter(Collider other) // Perguntar pro Roque se essa é a melhor maneira de fazer, ou criar um player controller para esses casos
    {
        if (other.CompareTag("EnemyAttack") && gameObject.CompareTag("Player"))// Erro: Collider das armas estão causando dano no player, por causa de serem filhas de um objeto com a tag Player
        {
           TakeDamage(other.GetComponentInParent<Enemy>().damageValue);
            
           playerHealthBar.fillAmount = currentHealth / maxHealth;
        }
    }


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
