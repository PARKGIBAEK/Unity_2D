using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{    
    public float maxSpeed;
    public float jumpPower;
    int coin;
    public GameManager gameManager;
    Rigidbody2D rb;
    CapsuleCollider2D capCollider;
    SpriteRenderer sprRenderer;
    Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()//키 조작은 FixedUpdate가 아닌 Update에서 하는것이 좋음, 이유는 FixedUpdate는 1초에 60번 다 돌아가지 않아서 가끔 키입력이 누락 될 때가 있음.
    {
        //Jump
        if (Input.GetButtonDown("Jump")&& !anim.GetBool("isJumping"))//점프중이 아니어야한다는 조건을 추가하기위해 GetBool함수 사용
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        
        //Stop Speed
        if(Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);//방향키 떼면 급감
        }
        //Sprite flipX
        sprRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;//좌방향키 누르면 이미지 뒤집기 true;

        //Animation Bool값 조작
        if (Mathf.Abs(rb.velocity.x) < 0.3f)//속도가 절대값 0.3보다 작으면 걷기 에니메이션 종료
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("테스터모드 : 캐릭터 위치 재설정");
            transform.position = new Vector2(-5f, -3.5f);
        }
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rb.velocity.x > maxSpeed)//오른쪽으로 이동 할때 최대 속도 제한 걸기
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);//y축은 기존 속도를 그대로 넣어주어 건드리지 않아야 함.
        else if (rb.velocity.x < -maxSpeed)//반대방향(왼쪽)으로 이동할 경우
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);

        //Landing Platform
        if (rb.velocity.y < 0)
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));//디버그 모드에서만 레이를 그려 줌
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("Platform"));//Layer이름이"Platform"에 해당하는 레이어만 스캔하겠다

            if (rayHit.collider != null)//Ray를 쏴서 맞았으면
            {
                if (rayHit.distance < 0.5f)
                {
                    //Debug.Log(rayHit.collider.name);
                    anim.SetBool("isJumping", false);
                }
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)//collision은 이 스크립트가 포함된 오브젝트가 충돌한 오브젝트를 가리킴
    {
        if(collision.gameObject.tag=="Monster")
        {
            //Attack
            if(rb.velocity.y<0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
            {
                Debug.Log("플레이어가 몬스터에게 타격을 받았습니다");
                OnDamaged(collision.transform.position);
            }
        }
        if(collision.gameObject.tag=="Trap")
        {
            OnDamaged(collision.transform.position);
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        //Health Down
        gameManager.health--;
        //change layer(Immortal active)
        gameObject.layer = 10;

        //view alpha
        sprRenderer.color = new Color(1, 1, 1, 0.4f);

        //Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc,1)*4, ForceMode2D.Impulse);

        //Animation
        anim.SetTrigger("damaged");

        Invoke("OffDamaged", 2f);

        
    }
    void OffDamaged()
    {
        gameObject.layer = 11;
        sprRenderer.color = new Color(1, 1, 1, 1);
    }
    void OnAttack(Transform enemy)
    {
        //Point
        gameManager.stagePoint += 100;
        //Reaction force
        rb.AddForce(Vector2.up * 7,ForceMode2D.Impulse);
        //Monster Die
        MonsterMove monMove = enemy.GetComponent<MonsterMove>();
        monMove.OnDamaged();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Gold")
        {//Point
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");
            if (isBronze)
            {
                gameManager.stagePoint += 100;
            }
            else if (isSilver)
            {
                gameManager.stagePoint += 50;
            }
            else if(isGold)
            {
                gameManager.stagePoint += 20;
            }
            //Object destroy
            Destroy(collision.gameObject);
            coin += 10;
        }
        else if(collision.gameObject.tag=="Finish")
        {
            //NextStage
            gameManager.NextStage();
        }
    }
    public void OnDie()
    {
        //Sprite alpha
        sprRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite flip Y
        sprRenderer.flipY = true;
        //Collider Disable
        capCollider.enabled = false;
        //Die Effect jump
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
}