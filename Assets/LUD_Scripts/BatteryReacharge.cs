using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryReacharge : MonoBehaviour
{
    public bool recharge;
    public GameObject raycastManager;
    public Image Fill;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Recharge());
    }

    IEnumerator Recharge()
    {
        while(recharge)
        {
            if (Fill.fillAmount < 1 && recharge)
            {
                Fill.fillAmount += 0.07f;
                yield return new WaitForSecondsRealtime(0.3f);
            }
        } 
    }
}
