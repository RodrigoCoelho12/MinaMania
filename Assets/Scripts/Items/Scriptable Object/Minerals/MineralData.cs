using UnityEngine;

[CreateAssetMenu(fileName = "MineralData", menuName = "Scriptable Objects/MineralData")]
public class MineralData : ItemData
{
    [Header("Mineral Properties")]
    public string history; 
    public MineralType mineralType;
}

public enum MineralType
{
    Meteorite,
    Crystal,
    Gemstone,
}

public static class MineralTypeExtensions
{
    public static string GetMineralTypeName(this MineralType type)
    {
        string typeName;
        switch (type)
        {
            case MineralType.Meteorite:
                typeName = "Meteorite";
                break;
            case MineralType.Crystal:
                typeName = "Crystal";
                break;
            case MineralType.Gemstone:
                typeName = "Gemstone";
                break;
            default:
                typeName = "Unknown";
                break;
        }
        return typeName;
    }
}