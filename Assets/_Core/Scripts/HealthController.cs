using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] float playerMaxHealth;
    [SerializeField] float playerCurrentHealth;
    [SerializeField] float playerRegenSpeed;
    [SerializeField] float enemyDamage;

    [SerializeField] Image playerHealtBar;
    [SerializeField] GameObject GameOverPanel;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerCurrentHealth -= enemyDamage * Time.deltaTime;
            playerHealtBar.fillAmount -= (enemyDamage * Time.deltaTime) / playerMaxHealth;
        }
    }


    // Update is called once per frame
    void Update()
    {
        PlayerHealthRegen();

    }

    public void PlayerHealthRegen()
    {
        if (playerCurrentHealth <playerMaxHealth)
        {
            playerHealtBar.fillAmount = Mathf.MoveTowards(playerHealtBar.fillAmount, 1f, Time.deltaTime * playerRegenSpeed);
            playerCurrentHealth = Mathf.MoveTowards(playerCurrentHealth / playerMaxHealth, 1f, Time.deltaTime * playerRegenSpeed) * playerMaxHealth;
        }
       
        if (playerCurrentHealth <= 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }

}
