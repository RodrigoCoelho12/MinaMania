using UnityEngine;

public class StoreController : MonoBehaviour
{

    [SerializeField] GameObject panelShop;

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
