using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.UIElements;
using System.Collections;

public class MinaAnimation : MonoBehaviour
{
    public UIDocument mainMenu;
    public Sprite[] frames;
    public float frameRate = 10f;
    private VisualElement imageElement;
    private int currentFrame;
    private float timer;

    void Start()
    {
        imageElement = mainMenu.rootVisualElement.Q("Titulo");
        //StartCoroutine(Animate());
    }

    /*IEnumerator Animate()
    {
        Debug.Log("BOMDIA");
        while (true)
        {
            imageElement.style.backgroundImage = new StyleBackground(frames[currentFrame]);
            currentFrame = (currentFrame + 1) % frames.Length;
            yield return new WaitForSeconds(1f / frameRate);
            Debug.Log("TESTE");
        }
    }*/

    public void TrocarSprite(ref int frame)
    {
        imageElement.style.backgroundImage = new StyleBackground(frames[frame]);
        frame ++;
    }
}
