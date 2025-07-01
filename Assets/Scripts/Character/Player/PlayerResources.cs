using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.UISystemProfilerApi;

public partial class Player
{
    [Header("Player Resources Properties")]

    [Header(" └─ Inventory")]
    public float dropCurrency;
    public TextMeshProUGUI currencyText;

    [Header(" └─ Weapons")]
    public PickaxeData currentPickaxeData;
    public WaterSprayData currentWaterSprayData;
    public DynamiteData currentDynamiteData;

    [Header(" └─ Weapon Instances")]
    public Pickaxe pickaxe;
    public WaterSpray waterSpray;
    public Dynamite dynamite;

    [Header(" └─ Weapon Models")]
    public GameObject PickaxeStandard;
    public GameObject PickaxeEvo1;
    public GameObject PickaxeEvo2;

    public GameObject WaterSprayStandard;
    public GameObject WaterSprayEvo1;
    public GameObject WaterSprayEvo2;

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
