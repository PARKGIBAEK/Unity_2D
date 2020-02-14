using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldScr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeString(string str)
    {
        if (str.Length == 0)
            return;
        Debug.Log("your input is : " + str[str.Length-1]);
    }

    public void endString(string str)
    {
        Debug.Log("final input is : " + str);
        this.GetComponent<InputField>().text = "";
    }
}
