using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeButton", menuName = "Scriptable Objects/UpgradeButton")]
public class UpgradeButton : ScriptableObject
{
    public string name;
    public string description;
    public int price;
}
