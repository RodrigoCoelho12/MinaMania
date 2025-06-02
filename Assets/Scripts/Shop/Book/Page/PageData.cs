using UnityEngine;
[CreateAssetMenu(menuName = "Collectible/Book Page")]
public class PageData : ScriptableObject
{
    public string title;
    public string storyText;
    public GameObject model3D; // Prefab ou referência ao modelo
}
