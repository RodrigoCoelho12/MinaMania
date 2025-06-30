using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagnetData", menuName = "Scriptable Objects/Item/Skill/MagnetData")]
public class MagnetData : SkillData
{
    public float magnetRange;
    public float magnetForce;
    public List<string> magnetAffectedItemTags;
    public LayerMask magnetLayerMask;

    public override string GetTooltip(ItemData previousData)
    {
        if (previousData != null && (previousData is MagnetData previousMagnetData))
        { 
            return base.GetTooltip(previousData) + $" ";
        }  
        else
        {
            Debug.LogError("Previous data is not of type MagnetData.");
            return null;
        }
    }
}
