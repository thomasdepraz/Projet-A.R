using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryFill : MonoBehaviour
{
    public Image Fill;
    void Start()
    {
        StartCoroutine(EmptyBattery());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EmptyBattery()
    {
        if (Fill.fillAmount>0)
        {
            Fill.fillAmount -= 0.05f;
            yield return new WaitForSeconds(3);
            StartCoroutine(EmptyBattery());
        }
    }
}
