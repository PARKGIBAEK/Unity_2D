using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public float coin;
    float RegenTime;
    public GameObject[] stages;
    public GameObject[] Monster;
    RectTransform warpHole;
    
    private void Awake()
    {
        if(GM==null)
        {
            GM = this;
        }
        else if(GM!=this)
        {
            Destroy(this.gameObject);
            Debug.Log("GameManager 오브젝트가 이미 생성되어있으므로, 소멸시킵니다.");
        }

        DontDestroyOnLoad(this.gameObject);//다른 씬으로 넘어가도 삭제 안되게하는 함수;

        stageIndex = stageIndex==0?0:stageIndex;
        health = 10;
        RegenTime = 5.0f;
        coin = 0;
        InvokeRepeating("SpawnMonster1",0,5.0f);
        warpHole = GameObject.Find("WarpHole").GetComponent<RectTransform>();
    }

    
    void SpawnMonster1()
    {
        if (GameObject.Find("Stage0") != null)
            stageIndex = 0;
        else if (GameObject.Find("Stage1") != null)
            stageIndex = 1;
        else if (GameObject.Find("Stage2") != null)
            stageIndex = 2;
        else if (GameObject.Find("Stage3") != null)
            stageIndex = 3;

        if (GameObject.Find("JumpingMonster(Clone)")==null)//Monster[0]의 몬스터가 Hierarchy에 존재하지 않으면
        {
            
            
            Instantiate(Monster[0], new Vector2(8f, 1.5f), new Quaternion(), GameObject.Find("Stage" + stageIndex).transform);            
        }
    }

    public IEnumerator NextStage()//깃발에 닿으면 워프홀을 점점 크게 만듬
    {
        PlayerController.player.rb.constraints= RigidbodyConstraints2D.FreezeAll;
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        InvokeRepeating("makeWarpHole", 0f, 0.032f);
        yield return new WaitForSeconds(1.0f);
        CancelInvoke();
        InvokeRepeating("removeWarpHole", 0f, 0.032f);
        yield return new WaitForSeconds(1.0f);
        CancelInvoke();
        
        if (stageIndex < stages.Length - 1)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            PlayerReposition();
        }
        else
        {
            //PlayerControl Lock
            Time.timeScale = 0;
            Debug.Log("마지막 스테이지 클리어!");
        }        
    }

    void makeWarpHole()
    {        
        if(warpHole.transform.localScale.x<=5 && warpHole.transform.localScale.y <= 5)
            warpHole.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        PlayerController.player.transform.localScale = new Vector3(0, 0, 1);
        //player.GetComponent<Transform>().localScale=new Vector3(0,0,1);
        GameObject.Find("HpBar").GetComponent<RectTransform>().localScale = new Vector3(0, 0, 1);
    }

    void removeWarpHole()
    {
        if (warpHole.transform.localScale.x >= 0 && warpHole.transform.localScale.y >= 0)
            warpHole.transform.localScale -= new Vector3(0.2f, 0.2f, 0);
    }
    public void HealthDown()
    {
        if (health > 0)
            health--;
        else
        {
           PlayerController.player.OnDie();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthDown();
            if (health >= 0)
            {
                collision.attachedRigidbody.velocity = Vector2.zero;
                collision.transform.position = new Vector3(0, 0, -1);
            }        
        }
    }

    void PlayerReposition()
    {
        PlayerController.player.transform.position = new Vector2(0, 0);
        PlayerController.player.VelocityZero();
        PlayerController.player.transform.localScale = new Vector3(1, 1, 1);
        PlayerController.player.rb.constraints = RigidbodyConstraints2D.None;
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        PlayerController.player.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GameObject.Find("HpBar").GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }
}
