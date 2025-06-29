using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private void Start()
    {
        Invoke("VoltarProMenu", 30f);
    }

    public void VoltarProMenu()
    {
        SceneManager.LoadScene(0);
    }





}
