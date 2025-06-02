using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Collectible/Book")]
public class BookData : ScriptableObject
{
    public string bookName;
    public List<ChapterData> chapters;
}
