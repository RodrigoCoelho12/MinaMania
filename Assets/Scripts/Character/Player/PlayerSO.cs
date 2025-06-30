using System.Collections.Generic;
using UnityEngine;

public class PlayerSO : MonoBehaviour
{
    public static PlayerSO Instance { get; private set; }

    [Header("Player Data")]
    public bool overrides;

    public PickaxeData pickaxeData;
    public WaterSprayData waterSprayData;
    public DynamiteData dynamiteData;
    public DashData dashData;
    public ExtraLifeData extraLifeData;
    public MagnetData magnetData;
    public float playerCurrency;
    public int hordeCount;
    public int score;

    public List<ItemData> discoveredSkills;
    public List<MineralData> discoveredMinerals;
    public List<WeaponData> discoveredWeapons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
