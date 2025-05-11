using UnityEngine;

public class ShopController : MonoBehaviour
{

    public GameObject panelShop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panelShop.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panelShop.SetActive(false);
        }
    }   
}
