using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Renderer))]
public class PageDataObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Data")]
    public PageData pageData;

    [Header("Visual Feedback")]
    public Color highlightColor = Color.blue;
    public float scaleMultiplier = 1.1f;

    private Renderer _renderer;
    private Color _originalColor;
    private Vector3 _originalScale;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            _originalColor = _renderer.material.color;
        }
        _originalScale = transform.localScale;
        Quaternion quaternion = Quaternion.Euler(-90, 90, 0);
        transform.rotation = quaternion;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _originalScale * scaleMultiplier;
        if (_renderer != null)
            _renderer.material.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _originalScale;
        if (_renderer != null)
            _renderer.material.color = _originalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (pageData == null)
        {
            Debug.LogWarning("No PageData assigned.");
            return;
        }

        Debug.Log($"Página clicada: {pageData.title}");
        BookManager bookManager = FindAnyObjectByType<BookManager>();
        bookManager?.SelectPage(pageData);
    }
}
