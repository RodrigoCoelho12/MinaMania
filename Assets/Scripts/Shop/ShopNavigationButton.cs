using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopNavigationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Shop Navigation Button Properties")]
    public Transform buttonTransform;
    public float hoverScaleFactor = 1.1f;
    public Camera shopCamera;
    public List<GameObject> shopPositionslist;

    private Vector3 originalPosition = new Vector3(0f, 2f, 0f); // Store the original position of the button
    private float rotationSpeed = 5f; // Speed of the camera rotation

    [Header("Navigation Settings")]
    public bool isRightButton = true; // Check this in the Inspector for the right button
    public static bool canRotate = true; // Static variable to control rotation state
    private static int currentIndex = 0;

    public void Awake()
    {
        StartCoroutine(SmoothLookAt(shopPositionslist[currentIndex].transform.position));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (shopCamera == null || shopPositionslist == null || shopPositionslist.Count == 0)
        {
            Debug.LogError("Shop camera or positions list is not properly assigned.");
            return;
        }
        if (!canRotate)
        {
            Debug.Log("Camera is currently rotating, please wait.");
            return; // Prevent further clicks while rotating
        }
        if (isRightButton)
        {
            currentIndex = (currentIndex + 1) % shopPositionslist.Count;
        }
        else
        {
            currentIndex = (currentIndex - 1 + shopPositionslist.Count) % shopPositionslist.Count;
        }

        Transform target = shopPositionslist[currentIndex].transform;
        StartCoroutine(SmoothLookAt(target.position));

    }

    private IEnumerator SmoothLookAt(Vector3 direction)
    {
        canRotate = false;
        if (shopCamera.transform.position != originalPosition)
        {
            while (Vector3.Distance(shopCamera.transform.position, originalPosition) > 0.1f)
            {
                shopCamera.transform.position = Vector3.Lerp(shopCamera.transform.position, originalPosition, Time.deltaTime * rotationSpeed);
                yield return null;
            }
        }
        shopCamera.transform.position = originalPosition; // Ensure camera is at the original position
        // Prevent further clicks during rotation
        Quaternion startRotation = shopCamera.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        float t = 0f;
        while (Quaternion.Angle(shopCamera.transform.rotation, targetRotation) > 0.1f)
        {
            t += Time.deltaTime * rotationSpeed;
            shopCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        // Snap to final rotation to avoid overshoot
        shopCamera.transform.rotation = targetRotation;
        canRotate = true; // Allow further clicks after rotation is complete
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonTransform != null)
        {
            buttonTransform.localScale *= hoverScaleFactor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonTransform != null)
        {
            buttonTransform.localScale /= hoverScaleFactor;
        }
    }
}
