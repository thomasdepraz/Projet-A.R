using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    public float energy;
    public float baseEnergy;
    public GameObject batteryFill;
    private BatteryFill fill;
    public Image energyBar;
    void Start()
    {
        fill = batteryFill.GetComponent<BatteryFill>();
    }

    // Update is called once per frame
    void Update()
    {
        if(energy <= 0)
        {
            Debug.Log("All energy transferred");
            Destroy(gameObject);
        }
        energyBar.fillAmount = energy / baseEnergy;
    }


    public void EnergyTransfer()
    {
        energy-- ;
        Debug.Log(energy);
    }
}
