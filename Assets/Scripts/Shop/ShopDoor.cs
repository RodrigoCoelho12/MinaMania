using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSO playerSO = FindAnyObjectByType<PlayerSO>();
            Player player = FindAnyObjectByType<Player>();

            playerSO.waterSprayData = player.currentWaterSprayData;
            playerSO.extraLifeData = player.currentExtraLifeData;
            playerSO.pickaxeData = player.currentPickaxeData;
            playerSO.magnetData = player.currentMagnetData;
            playerSO.dynamiteData = player.currentDynamiteData;
            playerSO.dashData = player.currentDashData;
            playerSO.playerCurrency = player.dropCurrency;
            
            DontDestroyOnLoad(playerSO);
            SceneManager.LoadScene(2);
        }
    }
}
