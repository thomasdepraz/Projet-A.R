using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryFill : MonoBehaviour
{
    
    public Image Panel;
    public Image Fill;
    public float fillAmount;
    [HideInInspector] public bool DontFill = false;
    
    public GameObject raycastManager;

    //Filter Color
    Color panelColor;
    float alpha;
    float r ; 
    float g ;
    float b ;
    void Start()
    {
        panelColor = Panel.color;
    }

    public void OnEnable()
    {
        fillAmount = 0.05f;
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
            Panel.color = panelColor;
        }
    }
    IEnumerator _BatteryGoDown()
    {
        while(!DontFill)
        {
            if (Fill.fillAmount >= 0)
            {
                Fill.fillAmount -= fillAmount;
                yield return new WaitForSeconds(3);
            }
            else
            {
                Fill.fillAmount = 0;
            }
        }
    }

}
