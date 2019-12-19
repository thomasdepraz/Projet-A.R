using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastManager : MonoBehaviour
{
    public bool DontFill;
    public bool Recharge;
    public float detectDistance;
    public LayerMask Battery;
    public LayerMask Enemy;
    public RaycastHit hit;
    public GameObject batteryFill;
    private BatteryFill fill;
    private BatteryReacharge Reacharge;
    public GameObject enemy;
    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        fill = batteryFill.GetComponent<BatteryFill>();
        enemyController = enemy.GetComponent<EnemyController>();
        Reacharge = batteryFill.GetComponent<BatteryReacharge>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectDistance, Battery))
        {
            hit.collider.gameObject.GetComponent<BatteryManager>().EnergyTransfer();//La charge de la batterie descend
            Reacharge.enabled = true;
            Recharge = true; //La charge du telephone augmente; 
            DontFill = true;
            

            Debug.Log("La batterie se recharge");
        }
        else
        {
            
            DontFill = false;
            Recharge = false;
            Reacharge.enabled = false;
        }

        if (Physics.Raycast(transform.position, transform.forward, detectDistance, Enemy))
        {
            //L'ennemi se stop, la batterie descend plus vite, incrémenter un compteur l'ennemi se tp 
            enemyController.canMove = false;
            Debug.Log("Enemy arreté");
        }
        else
        {
            enemyController.canMove = true;
            Debug.Log("Enemy en mouvement");

        }
    }
}
