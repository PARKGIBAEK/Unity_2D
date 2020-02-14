using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GaugeBar : MonoBehaviour
{
    Image myImage = null;
    public GameObject cursorObj = null;
    float gauge=0;
    bool wise = false;
    private void Start()
    {
        myImage = GetComponent<Image>();
        myImage.fillMethod = Image.FillMethod.Radial360;
        myImage.fillClockwise = true;
        myImage.fillAmount = 0;
    }
    private void Update()
    {
        if (!wise)
            myImage.fillAmount += Time.deltaTime * 0.5f;
        else
            myImage.fillAmount -= Time.deltaTime * 0.5f;
        
        if (myImage.fillAmount >= 1)
        {
            wise = true;            
        }
        else if(myImage.fillAmount<=0)
        {
            wise = false;
        }
        
        //cursorObj.transform.eulerAngles = new Vector3(0,0,-myImage.fillAmount*360f);//오일러 앵글은 vector3로 사용해야함
        cursorObj.transform.eulerAngles = -Vector3.forward*360*myImage.fillAmount;
    }
}
