using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryFill : MonoBehaviour
{
    
    public Image Panel;
    bool DontFill=false;
    public Image Fill;
    private float fillAmount = 0.05f;
    public GameObject raycastManager;
    [HideInInspector] public bool isCharging = false;
    void Start()
    {
        //StartCoroutine(EmptyBattery());
        //StartCoroutine(FillBattery());
        StartCoroutine(_BatteryGoDown());
        
        
    }

    // Update is called once per frame
    void Update()
    {
        DontFill = raycastManager.GetComponent<RaycastManager>().DontFill;
        if (Fill.fillAmount<=0)
        {
            var opacity = Panel.color;
            opacity.a = 0.75f;
            opacity.r =0f;
            opacity.g = 0f;
            opacity.b = 0f;
            Panel.color = opacity;
        }

    }
    IEnumerator _BatteryGoDown()
    {
        if (Fill.fillAmount > 0 && DontFill==false)
        {
            Fill.fillAmount -= 0.05f;
            yield return new WaitForSeconds(3);
            
        }
        
        StartCoroutine(_BatteryGoDown());
    }
    
    //IEnumerator EmptyBattery()
    //{
    //    if (!isCharging)
    //    {
    //        if (Fill.fillAmount > 0)
    //        {
    //            Fill.fillAmount -= 0.05f;
    //            yield return new WaitForSeconds(1);
    //            StartCoroutine(EmptyBattery());
    //        }
    //    }
    //     StartCoroutine(EmptyBattery());
    //}

    //IEnumerator FillBattery()
    //{
    //    if (isCharging)
    //    {
    //        if (Fill.fillAmount < 1)
    //        {
    //            Fill.fillAmount += 0.05f;
    //            yield return null;
    //            StartCoroutine(FillBattery());
    //        }
    //    }
    //    StartCoroutine(FillBattery());
    //}


}
