using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleScript : MonoBehaviour
{
    public Toggle t1, t2, t3;
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    
    public void toggleClicked(bool b)
    {        
        Debug.Log(b);
        if (b == true)
        {
            Toggle myToggle = this.GetComponent<Toggle>();
            if (t1 != myToggle)
                t1.isOn = false;
            if (t2 != myToggle)
                t2.isOn = false;
            if (t3 != myToggle)
                t3.isOn = false;
        }
    }

}