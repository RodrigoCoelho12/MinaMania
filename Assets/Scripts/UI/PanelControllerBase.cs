using UnityEngine;
using UnityEngine.UIElements;

public class PanelControllerBase : MonoBehaviour
{
    protected VisualElement rootUi;
    protected Button quitButton;
    
    protected virtual void OnEnable()
    {
        rootUi = GetComponent<UIDocument>().rootVisualElement;

        quitButton = rootUi.Q<Button>("QuitButton");
        quitButton.clicked += OnQuitButtonClicked;


    }

    protected virtual void OnDisable()
    {
        quitButton.clicked -= OnQuitButtonClicked;

    }
   
    protected virtual void OnQuitButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
