using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UISystemProfilerApi;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] Pickaxe pickaxe;
    [SerializeField] WaterSpray waterSpray;
    [SerializeField] BombLauncher bombLauncher;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickaxe = transform.GetChild(0).gameObject.GetComponent<Pickaxe>();
        waterSpray = transform.GetChild(1).gameObject.GetComponent<WaterSpray>();
        bombLauncher = transform.GetChild(2).gameObject.GetComponent<BombLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        pickaxe.Attack();
        
        waterSpray.Attack();

        bombLauncher.Attack();
    }

}
