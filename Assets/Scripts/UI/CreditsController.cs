using UnityEngine;
using UnityEngine.UIElements;

public class CreditsController : PanelControllerBase
{
    protected override void OnQuitButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
