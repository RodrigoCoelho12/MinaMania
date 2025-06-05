using UnityEngine;
[CreateAssetMenu(menuName = "Collectible/Book Page")]
public class PageData : ScriptableObject
{
    public string title;
    public int id;
    public string storyText;
    public GameObject model3D; // Prefab ou referência ao modelo
}
