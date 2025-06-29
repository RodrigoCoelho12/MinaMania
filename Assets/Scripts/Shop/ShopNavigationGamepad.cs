using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopNavigationGamepad : MonoBehaviour
{
    [Header("Shop Navigation Button Properties")]
    public Camera shopCamera;
    public List<GameObject> shopPositionslist;

    private Vector3 originalPosition = new Vector3(0f, 2f, 0f); // Store the original position of the button
    private float rotationSpeed = 5f; // Speed of the camera rotation

    [Header("Navigation Settings")]
    public static bool canRotate = true; // Static variable to control rotation state
    public int currentIndex = 0;

    public Vector3[] camPos = new Vector3[2]; // index 0 = standard, index 1 = close
    private bool isMoving;

    public Camera currentCamera;

    public void Awake()
    {
        StartCoroutine(SmoothLookAt(shopPositionslist[currentIndex].transform.position));
    }

    private void Update()
    {
        RotateCamera();
        Interact();
    }
    public void RotateCamera()
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

        if (ShopInputManager.instance.RotateRightInput)
        {
            currentIndex = (currentIndex + 1) % shopPositionslist.Count;
            
            Transform target = shopPositionslist[currentIndex].transform;
            StartCoroutine(SmoothLookAt(target.position));
        }
        else if (ShopInputManager.instance.RotateLeftInput)
        {
            currentIndex = (currentIndex - 1 + shopPositionslist.Count) % shopPositionslist.Count;
            
            Transform target = shopPositionslist[currentIndex].transform;
            StartCoroutine(SmoothLookAt(target.position));
        }

    }

    public void Interact()
    {
        if (currentIndex == 0 && ShopInputManager.instance.SubmitInput)
        {
            PlayerSO playerSO = FindAnyObjectByType<PlayerSO>();
            DontDestroyOnLoad(playerSO);

            SceneManager.LoadScene(1);
        }
        if(currentIndex == 1  && ShopInputManager.instance.SubmitInput)
        {

            if (!isMoving)
            {
                StartMoving(camPos[1], 1f, currentCamera.gameObject);
            }

        }
        else
        {

        }
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
    public void StartMoving(Vector3 newPosition, float timeToMove, GameObject gameObject)
    {
        StartCoroutine(MoveCameraToPosition(newPosition, timeToMove, gameObject));
    }

    private IEnumerator MoveCameraToPosition(Vector3 targetPosition, float duration, GameObject gameObject)
    {
        isMoving = true;
        Vector3 startPosition = gameObject.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.position = targetPosition;
        isMoving = false;
    }
}
