using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DontDestroyOnLoad(FindAnyObjectByType<PlayerSO>());
            SceneManager.LoadScene(2);
        }
    }
}
