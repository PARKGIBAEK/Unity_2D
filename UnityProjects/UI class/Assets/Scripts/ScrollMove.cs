using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMove : MonoBehaviour
{
    public Image img;//바꿀 이미지 객체
    public Sprite spr;//변경 후 이미지
    // Start is called before the first frame update
    void Start()
    {
        ScrollRect scrRect = this.GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveScroll(Vector2 v)
    {
        if(v.y<0.5f)
        {
            img.sprite = spr;
        }
        Debug.Log(v);
    }
}
