using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//이벤트 시스템을 상속받기 위함
public class ButtonController : MonoBehaviour,IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("script Enter");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnClick()
    {
        Debug.Log("button is clicked");
    }

    public void btnClick2()
    {
        Debug.Log("button is clicked 2");
    }
    public void myMouseEnter()
    {
        Debug.Log("Enter");
    }
    public void myMouseExit()
    {
        Debug.Log("Exit");
    }
    public void myMouseDown()
    {
        Debug.Log("Down");
    }
    public void myMouseUp()
    {
        Debug.Log("Up");
    }
    public void myMouseClick()
    {
        Debug.Log("Click");
    }
    public void myMouseDrag()
    {
        Debug.Log("Drag");
    }
}
