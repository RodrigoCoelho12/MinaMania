using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    public  void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(1);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        this.GetComponent<Renderer>().material.color = Color.grey;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        this.GetComponent<Renderer>().material.color = Color.cyan;
    }
}
