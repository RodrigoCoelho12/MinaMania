using UnityEngine;

[CreateAssetMenu(fileName = "ExtraLifeData", menuName = "Scriptable Objects/Item/Skill/ExtraLifeData")]
public class ExtraLifeData : SkillData
{
    public int extraLives = 1;
    public override string GetTooltip(ItemData previousData)
    { 
        if (previousData != null && (previousData is ExtraLifeData previousExtraLifeData))
        { 
            return "ExtraLife";
        }  
        else
        {
            Debug.LogError("Previous data is not of type ExtraLife.");
            return null;
        }
    }
}
