using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeButton", menuName = "Scriptable Objects/UpgradeButton")]
public class UpgradeButton : ScriptableObject
{
    public string _name;
    public string description;
    public int price;
}
