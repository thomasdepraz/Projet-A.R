using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    public float energy;
    public GameObject batteryFill;
    private BatteryFill fill;
    public GameObject barFill;
    private Image barFillImage;
    void Start()
    {
        fill = batteryFill.GetComponent<BatteryFill>();
        barFill = GameObject.FindGameObjectWithTag("BatteryUI");
        barFillImage = barFill.GetComponent<Image>();
        barFill.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(energy <= 0)
        {
            Debug.Log("All energy transferred");
            Destroy(gameObject);
            gameObject.GetComponent<AudioSource>().mute = true;
        }   
    }


    public void EnergyTransfer()
    {
        gameObject.GetComponent<AudioSource>().mute = false;
        barFillImage.fillAmount = energy / 100;
        energy-- ;
        Debug.Log(energy);
    }
}
