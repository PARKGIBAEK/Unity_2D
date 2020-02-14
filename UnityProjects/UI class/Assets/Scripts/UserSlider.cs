using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //unity에서 ui사용을 위해 추가
using UnityEngine.EventSystems; // 비 버튼 객체에서 버튼 구현을 하기 위해 추가

public class UserSlider : MonoBehaviour, IPointerDownHandler, IDragHandler // EventSystems사용시 IPointerDownHandler 상속, 드래그 사용시 IDragHandler 상속후, 정의
{
    void Update()
    {
        float amountX = myFilled.fillAmount * this.GetComponent<Image>().rectTransform.sizeDelta.x;//sizeDelta.x 는 width값을 불러오는 것
        myHandle.rectTransform.position = this.GetComponent<Image>().rectTransform.position + Vector3.right * amountX;
    }

    Vector2 beforePos;
    public Image myFilled;
    public Image myHandle;

    public void OnPointerDown(PointerEventData eventData)
    {
        beforePos = eventData.position;

        float myX = this.transform.position.x;

        float filledValue = beforePos.x - myX;

        float sizeX = this.GetComponent<Image>().rectTransform.sizeDelta.x;

        myFilled.fillAmount = filledValue / sizeX;
    }
    Vector2 movedPos;

    public void OnDrag(PointerEventData eventData)
    {
        movedPos = eventData.position;

        float movedXpos = movedPos.x - beforePos.x;
        float movedValue = movedXpos / this.GetComponent<Image>().rectTransform.sizeDelta.x;

        float fixedValue = myFilled.fillAmount * movedValue;

        if (fixedValue > 1)
            fixedValue = 1;
        else if (fixedValue < 0)
            fixedValue = 0;

        myFilled.fillAmount += movedValue;

        beforePos = movedPos;

    }
}