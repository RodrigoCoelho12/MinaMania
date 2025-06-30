using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleHover : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Vector3 originalScale;
    private void Start()
    {
        originalScale =  transform.localScale;
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale *= 1.1f;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
