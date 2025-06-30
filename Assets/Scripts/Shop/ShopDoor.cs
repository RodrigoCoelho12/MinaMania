using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();


            PlayerSO.Instance.waterSprayData = player.currentWaterSprayData;
            PlayerSO.Instance.extraLifeData = player.currentExtraLifeData;
            PlayerSO.Instance.pickaxeData = player.currentPickaxeData;
            PlayerSO.Instance.magnetData = player.currentMagnetData;
            PlayerSO.Instance.dynamiteData = player.currentDynamiteData;
            PlayerSO.Instance.dashData = player.currentDashData;
            PlayerSO.Instance.playerCurrency = player.dropCurrency;
            PlayerSO.Instance.hordeCount = FindAnyObjectByType<EnemyHordeSpawner>().hordeCount;
            PlayerSO.Instance.score = player.score;

            foreach(var mineral in player.discoveredMinerals)
            {
                PlayerSO.Instance.discoveredMinerals.Add(mineral);
            }
            foreach (var skill in player.discoveredSkills)
            {
                PlayerSO.Instance.discoveredSkills.Add(skill);
            }
            foreach (var weapon in player.discoveredWeapons)
            {
                PlayerSO.Instance.discoveredWeapons.Add(weapon);
            }

            SceneManager.LoadScene(2);
        }
    }
}
