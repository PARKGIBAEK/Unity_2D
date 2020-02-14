using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController can;
    Slider hpBar;
    Image hpColor;
    Image spaceWarp;

    void Awake()
    {
        if (can == null)
        {
            can = this;
        }
        else if (can != this)
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
        Vector3 playerPos = PlayerController.player.transform.position;
        Vector3 movedPos = Camera.main.WorldToScreenPoint(playerPos) + Vector3.up * 40;
        CanvasController.can.transform.position = movedPos;
        hpBar.value = GameObject.Find("GameManager").GetComponent<GameManager>().health;
        hpColor.color = Color.Lerp(Color.red, Color.green, hpBar.value / 10);
        //if (Input.GetKey(KeyCode.LeftArrow))
        //    TargetObj.transform.Translate(Vector3.left);
        //else if (Input.GetKey(KeyCode.RightArrow))
        //    TargetObj.transform.Translate(Vector3.right);
    }
}
