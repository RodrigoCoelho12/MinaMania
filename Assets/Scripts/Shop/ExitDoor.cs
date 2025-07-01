using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {

        SceneManager.LoadScene(1);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).transform.localScale = new Vector3(1.38f, 1.38f, 1.38f);
        //this.GetComponent<Renderer>().material.color = Color.grey;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(0).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        // this.GetComponent<Renderer>().material.color = Color.cyan;
    }
}
