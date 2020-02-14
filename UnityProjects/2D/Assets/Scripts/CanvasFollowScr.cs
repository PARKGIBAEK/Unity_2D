using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFollowScr : MonoBehaviour
{
    public static CanvasFollowScr can;
    public GameObject TargetObj;//따라다닐 대상
    public GameObject followUI;//따라다니는 UI
    Slider hpBar;
    Image hpColor;
    Image spaceWarp;
    
    void Awake()
    {
        if(can==null)
        {
            can = this;
        }
        else if(can!=this)
        {
            Destroy(this.gameObject);
            Debug.Log("Canvas 오브젝트가 이미 생성되어있으므로, 소멸시킵니다.");
        }
        DontDestroyOnLoad(this.gameObject);


        hpBar = transform.Find("HpBar").GetComponent<Slider>();
        hpColor = hpBar.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
        spaceWarp = transform.Find("WarpHole").GetComponent<Image>();
        spaceWarp.transform.localScale = new Vector3(0, 0, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        spaceWarp.transform.position = Camera.main.WorldToScreenPoint(GameObject.Find("Player").transform.position);
        Vector3 targetPos = TargetObj.transform.position;
        Vector3 movedPos = Camera.main.WorldToScreenPoint(targetPos)+Vector3.up*40;
        followUI.transform.position = movedPos;
        hpBar.value = GameObject.Find("GameManager").GetComponent<GameManager>().health;
        hpColor.color=Color.Lerp(Color.red, Color.green, hpBar.value/10);
    }
}
