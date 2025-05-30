using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.VisualScripting;
public class BookVisionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector3 lookAtBookUIPosition;
    public Camera shopCamera;
    private Vector3 originalPosition = new Vector3(0f, 2f, 0f); // Store the original position of the camera
    private float rotationSpeed = 5f; // Speed of the camera rotation
    public bool canRotate = true; // Allow rotation by default

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;  
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (shopCamera != null)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (shopCamera != null)
        {
            shopCamera.transform.position = lookAtBookUIPosition; // Reset camera position
        }
    }
}
