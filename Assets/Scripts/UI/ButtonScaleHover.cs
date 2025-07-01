using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleHover : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    
    private void Awake()
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
    
   public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
