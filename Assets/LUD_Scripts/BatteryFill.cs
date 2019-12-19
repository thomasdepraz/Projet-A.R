using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryFill : MonoBehaviour
{
    public Image Fill;
    private float fillAmount = 0.05f;
    [HideInInspector] public bool isCharging = false;
    void Start()
    {
        StartCoroutine(EmptyBattery());
        StartCoroutine(FillBattery());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator EmptyBattery()
    {
        if(!isCharging)
        {
            if (Fill.fillAmount > 0)
            {
                Fill.fillAmount -= 0.05f;
                yield return new WaitForSeconds(1);
                StartCoroutine(EmptyBattery());
            }
        }
    }

    IEnumerator FillBattery()
    {
        if (isCharging)
        {
            if (Fill.fillAmount < 1)
            {
                Fill.fillAmount += 0.05f;
                yield return null;
                StartCoroutine(FillBattery());
            }
        }       
    }


}
