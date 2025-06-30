using UnityEngine;

public class PlayerSO : MonoBehaviour
{
    public PlayerSO Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Garante que só um existe
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // Opcional: persiste entre cenas
    }

    public bool overrides;
    public PickaxeData pickaxeData;
    public WaterSprayData waterSprayData;
    public DynamiteData dynamiteData;
    public DashData dashData;
    public ExtraLifeData extraLifeData;
    public MagnetData magnetData;
    public float playerCurrency;
    public int hordeCount;
}
