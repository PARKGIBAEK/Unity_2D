using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI 컴포넌트 사용하기 위해 추가

public class textScr : MonoBehaviour
{
    Text tx;
    float time;
    int time2;
    string a;
    public Font myFont = null;//전역변수 앞에 public을 붙이면 unity editor에서 볼 수 있게 됨
    void Start()
    {        
        tx = GetComponent<Text>();
        //tx.font = (Font)Resources.Load("BMDOHYEON_ttf");//폰트 변경 (확장자를 적으면 안됨)
        tx.font = myFont;
        tx.text = "<b>스크립트</b>로 바꾼 텍스트입니다";//텍스트 변경 (리치 텍스트 이용함)
        tx.fontStyle = FontStyle.BoldAndItalic;//폰트 스타일
        tx.fontSize = 50;//폰트 사이즈
        tx.lineSpacing = 1.5f;//줄 간격
        tx.supportRichText = true;//리치텍스트 지원 여부
        tx.alignment = TextAnchor.MiddleCenter;
        tx.horizontalOverflow = HorizontalWrapMode.Overflow;
        tx.verticalOverflow = VerticalWrapMode.Overflow;
        //tx.resizeTextMaxSize = 500;
        //tx.resizeTextMinSize = 24;
        tx.color = Color.blue;        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        time2 = (int)time;
        if (time2 % 8 == 0)
        {
            tx.alignment = TextAnchor.UpperCenter;
        }
        else if (time2 % 7 == 0)
        {
            tx.alignment = TextAnchor.UpperRight;
        }
        else if (time2 % 6 == 0)
        {
            tx.alignment = TextAnchor.MiddleRight;
        }
        else if (time2 % 5 == 0)
        {
            tx.alignment = TextAnchor.LowerRight;
        }
        else if (time2 % 4 == 0)
        {
            tx.alignment = TextAnchor.LowerCenter;
        }
        else if (time2 % 3 == 0)
        {
            tx.alignment = TextAnchor.LowerLeft;
        }
        else if (time2 % 2 == 0)
        {
            tx.alignment = TextAnchor.MiddleLeft;
        }
        else if (time2 % 1 == 0)
        {
            tx.alignment = TextAnchor.UpperLeft;
        }
        else
        {
            tx.alignment = TextAnchor.MiddleCenter;
            tx.color = Color.red;
        }
        //a = time2.ToString();
        tx.text = "<color=yellow>"+time2.ToString()+"</color>";
        //tx.text = a;
    }
}
