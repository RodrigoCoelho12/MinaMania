using UnityEngine;

[CreateAssetMenu(fileName = "DashData", menuName = "Scriptable Objects/Item/Skill/DashData")]
public class DashData : SkillData
{
    public float dashSpeed;            
    public float dashDuration;        
    public float dashCooldown;
    public override string GetTooltip(ItemData previousData)
    {
        if (previousData != null && (previousData is DashData previousDashData))
        { 
            return "Dash";
        }  
        else
        {
            Debug.LogError("Previous data is not of type DashData.");
            return null;
        }
    }
}
