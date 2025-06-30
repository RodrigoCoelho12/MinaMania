using TMPro;
using UnityEngine;

public class MineralController : MonoBehaviour
{
    public MineralData mineralData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = FindAnyObjectByType<Player>();

            player.discoveredMinerals.Add(mineralData);

            Destroy(this.gameObject);
        }
    }
}
