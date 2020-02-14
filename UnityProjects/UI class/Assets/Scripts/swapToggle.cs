using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swapToggle : MonoBehaviour
{
    public Sprite img_true, img_false;
    public Image backGround;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void toggleFunc(bool b)
    {

        Toggle myToggle = this.GetComponent<Toggle>();
        if (b == true)
        {
            backGround.sprite = img_true;
        }
        else if (b == false)
        {
            backGround.sprite = img_false;
        }
    }
}
