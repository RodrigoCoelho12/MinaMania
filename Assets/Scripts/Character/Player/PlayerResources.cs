using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UISystemProfilerApi;

public partial class Player
{
    [Header("Player Resources Properties")]

    [Header(" └─ Inventory")]
    public float coins;

    [Header(" └─ Weapons")]
    public DynamiteData currentDynamiteData;
    public PickaxeData currentPickaxeData;
    public WaterSprayData currentWaterSprayData;

    [Header("   └─ Discovered Weapons")]
    public List<WeaponData> discoveredWeapons;

    [Header(" └─ Skills")]
    //Fix
    public DashData currentDashData;
    public MagnetData currentMagnetData;
    public ExtraLifeData currentExtraLifeData;

    [Header("   └─ Skills Enable Properties")]
    public bool hasDash;
    public bool hasMagnet;
    public bool hasExtraLife;

    [Header("   └─ Discovered Skills")]
    //Fix
    public List<ItemData> discoveredSkills;

    [Header(" └─ Discovered Minerals")]
    public List<MineralData> discoveredMinerals;

}
