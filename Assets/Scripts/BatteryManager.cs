using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    public float energy; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(energy <= 0)
        {
            Debug.Log("All energy transferred");
            Destroy(gameObject);
        }
    }

    public void EnergyTransfer()
    {
        energy-- ;
        Debug.Log(energy);
    }
}
