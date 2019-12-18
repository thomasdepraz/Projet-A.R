using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastManager : MonoBehaviour
{

    public float detectDistance;
    public LayerMask Battery;
    public LayerMask Enemy;
    public RaycastHit hit;
    public GameObject batteryFill;
    private BatteryFill fill;

    // Start is called before the first frame update
    void Start()
    {
        fill = batteryFill.GetComponent<BatteryFill>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectDistance, Battery))
        {
            hit.collider.gameObject.GetComponent<BatteryManager>().EnergyTransfer();//La charge de la batterie descend
            fill.isCharging = true; //La charge du telephone augmente; 


            Debug.Log("La batterie se recharge");
        }
        else
        {
            fill.isCharging = false;
        }

        if (Physics.Raycast(transform.position, transform.forward, detectDistance, Enemy))
        {
            //L'ennemi se stop, la batterie descend plus vite, incrémenter un compteur l'ennemi se tp 
            Debug.Log("Enemy arreté");
        }
        else
        {
            
        }
    }
}
