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
    private BatteryReacharge Reacharge;
    public GameObject enemy;
    private EnemyController enemyController;
    private float teleportCooldown;
    public GameObject batteryFillUI;

    // Start is called before the first frame update
    void Start()
    {
        fill = batteryFill.GetComponent<BatteryFill>();
        Reacharge = batteryFill.GetComponent<BatteryReacharge>();

        enemyController = enemy.GetComponent<EnemyController>();
        teleportCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectDistance, Battery))
        {
            batteryFillUI.SetActive(true);
            hit.collider.gameObject.GetComponent<BatteryManager>().EnergyTransfer();//La charge de la batterie descend

            fill.DontFill = true;//La charge du telephone ne baisse plus; 
            fill.enabled = false;

            Reacharge.recharge = true; //La charge du telephone augmente; 
            Reacharge.enabled = true;
        }
        else
        {
            batteryFillUI.SetActive(false);
            fill.DontFill = false;//La charge du telephone baisse;
            fill.enabled = true;

            Reacharge.enabled = false;//La charge du telephone n'augmente plus; 
            Reacharge.recharge = false;
        }

        if (Physics.Raycast(transform.position, transform.forward, detectDistance, Enemy))
        {
            //L'ennemi se stop, la batterie descend plus vite, incrémenter un compteur l'ennemi se tp 
            enemyController.canMove = false;
            Debug.Log("Enemy arreté");
            teleportCooldown++;
            if(teleportCooldown > 300)
            {
                enemy.transform.position = (transform.position - enemy.transform.position) * 3;
            }
            fill.fillAmount = 0.07f;
        }
        else
        {
            teleportCooldown = 0;
            enemyController.canMove = true;
            Debug.Log("Enemy en mouvement");
            fill.fillAmount = 0.05f;
        }
    }
}
