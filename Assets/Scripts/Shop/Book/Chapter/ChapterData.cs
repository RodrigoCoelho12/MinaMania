using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Collectible/Chapter")]
public class ChapterData : ScriptableObject
{
    public string chapterName;
    public List<PageData> pages;
}
