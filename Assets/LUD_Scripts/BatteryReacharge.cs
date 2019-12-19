using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryReacharge : MonoBehaviour
{
    bool recharge;
    public GameObject raycastManager;
    public Image Fill;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Recharge());
    }

    // Update is called once per frame
    void Update()
    {
        recharge = raycastManager.GetComponent<RaycastManager>().Recharge;
    }

    IEnumerator Recharge()
    {
        if (Fill.fillAmount < 1 && recharge==true)
        {
            Fill.fillAmount += 0.05f;
            yield return null;
            StartCoroutine(Recharge());
        }
        
    }
}
