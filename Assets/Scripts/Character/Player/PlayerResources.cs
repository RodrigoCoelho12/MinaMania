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
    public PickaxeData currentPickaxeData;
    public WaterSprayData currentWaterSprayData;
    public DynamiteData currentDynamiteData;

    [Header(" └─ Weapon Instances")]
    public Pickaxe pickaxe;
    public WaterSpray waterSpray;
    public Dynamite dynamite;


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
