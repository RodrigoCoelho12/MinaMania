using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UISystemProfilerApi;

public partial class Player : Character
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
    // [SerializeField] Pickaxe pickaxe;
    // [SerializeField] WaterSpray waterSpray;
    // [SerializeField] BombLauncher bombLauncher;
    
    // void Start()
    // {
    //     pickaxe = transform.GetChild(0).gameObject.GetComponent<Pickaxe>();
    //     waterSpray = transform.GetChild(1).gameObject.GetComponent<WaterSpray>();
    //     bombLauncher = transform.GetChild(2).gameObject.GetComponent<BombLauncher>();
    // }

    // void Update()
    // {
    //     pickaxe.Attack();
        
    //     waterSpray.Attack();

    //     bombLauncher.Attack();
    // }

}
