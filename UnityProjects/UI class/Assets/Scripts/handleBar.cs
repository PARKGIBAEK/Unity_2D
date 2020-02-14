using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class handleBar : MonoBehaviour
{
    Image img;
    //Image img2;
    private void Start()
    {
        img = GetComponentInParent<Image>();
        img.fillMethod = Image.FillMethod.Radial360;
        img.fillOrigin = 1;
        img.fillClockwise = true;
        img.fillAmount = 0;
    }
    private void Update()
    {
        img.fillAmount = img.GetComponentInParent<Image>().fillAmount;
        
    }
}
