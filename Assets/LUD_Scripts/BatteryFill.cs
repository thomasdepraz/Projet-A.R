using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryFill : MonoBehaviour
{
    
    public Image Panel;
    public Image Fill;
    private float fillAmount = 0.05f;
    [HideInInspector] public bool DontFill = false;
    
    public GameObject raycastManager;

    void Start()
    {
        //StartCoroutine(_BatteryGoDown());
    }

    public void OnEnable()
    {
        StartCoroutine(_BatteryGoDown());
    }
    // Update is called once per frame
    void Update()
    {  
        if (Fill.fillAmount<=0)
        {
            var opacity = Panel.color;
            opacity.a = 0.75f;
            opacity.r = 0f;
            opacity.g = 0f;
            opacity.b = 0f;
            Panel.color = opacity;
        }
        else
        {
            //reset colors
        }
    }
    IEnumerator _BatteryGoDown()
    {
        while(!DontFill)
        {
            if (Fill.fillAmount >= 0)
            {
                Fill.fillAmount -= 0.05f;
                yield return new WaitForSeconds(3);
            }
            else
            {
                Fill.fillAmount = 0;
            }
        }
    }

    private void PanelColor()
    {

    }
}
