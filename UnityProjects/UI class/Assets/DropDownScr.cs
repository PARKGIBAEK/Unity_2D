using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropDownScr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dropChange(int value)
    {
        Dropdown dd = this.GetComponent<Dropdown>();

        //Debug.Log("selected item is" + value);
        Debug.Log("selected item is  " + dd.options[value].text);
    }
}