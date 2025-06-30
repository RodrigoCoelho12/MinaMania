using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/Item/Skill/SkillData")]
public class SkillData : ItemData
{
    public override string GetTooltip(ItemData previousData)
    {
        if (previousData != null && (previousData is SkillData previousSkillData))
        { 
            return $"{description} | Custo: {itemPrice}";
        }  
        else
        {
            Debug.LogError("Previous data is not of type SkillData.");
            return null;
        }
    }
}
