using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSO playerSO = FindAnyObjectByType<PlayerSO>();


            Player player = other.GetComponent<Player>();


            PlayerSO.Instance.waterSprayData = player.currentWaterSprayData;
            PlayerSO.Instance.extraLifeData = player.currentExtraLifeData;
            PlayerSO.Instance.pickaxeData = player.currentPickaxeData;
            PlayerSO.Instance.magnetData = player.currentMagnetData;
            PlayerSO.Instance.dynamiteData = player.currentDynamiteData;
            PlayerSO.Instance.dashData = player.currentDashData;
            PlayerSO.Instance.playerCurrency = player.dropCurrency;
            PlayerSO.Instance.hordeCount = FindAnyObjectByType<EnemyHordeSpawner>().hordeCount;
            
            SceneManager.LoadScene(2);
        }
    }
}
