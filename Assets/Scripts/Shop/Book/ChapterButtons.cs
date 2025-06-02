using UnityEngine;
using UnityEngine.EventSystems;

public class ChapterButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ChapterData chapterData;

    private BookManager bookManager;
    private int chapterIndex;

    public void Initialize(BookManager manager, int index)
    {
        bookManager = manager;
        chapterIndex = index;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.1f;
        if (TryGetComponent<Renderer>(out var renderer))
            renderer.material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!bookManager.isSelected[chapterIndex])
        {
            transform.localScale = Vector3.one;
            if (TryGetComponent<Renderer>(out var renderer))
                renderer.material.color = Color.white;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        bookManager.SelectChapter(chapterIndex, chapterData);
    }

    public void Highlight()
    {
        transform.localScale = Vector3.one * 1.1f;
        if (TryGetComponent<Renderer>(out var renderer))
            renderer.material.color = Color.yellow;
    }

    public void ResetAppearance()
    {
        transform.localScale = Vector3.one;
        if (TryGetComponent<Renderer>(out var renderer))
            renderer.material.color = Color.white;
    }
}
