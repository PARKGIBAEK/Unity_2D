using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player = null;
    public float maxSpeed;
    public float jumpPower;
    
    bool hit;
    public int damage;
    public Rigidbody2D rb;
    public CapsuleCollider2D capCollider;
    public SpriteRenderer sprRenderer;
        
    public Animator anim;
    
    
    private void Awake()
    {
        if(player==null)
        {
            player = this;
        }
        else if(player!=this)
        {
            Destroy(this.gameObject);
            Debug.Log("Player 오브젝트가 이미 생성되어있으므로, 소멸시킵니다.");
        }
        DontDestroyOnLoad(this.gameObject);

        maxSpeed =maxSpeed==0? 4.5f:maxSpeed;
        jumpPower =jumpPower==0? 12.0f:jumpPower;
        damage = 1;
        hit = false;        
        rb = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Jump")&&!anim.GetBool("isJumping"))//점프구현
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        //좌우방향키에 따른 움직임 구현
        float h = Input.GetAxisRaw("Horizontal");
        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rb.velocity.x>maxSpeed)//최대 속도 제한 걸기
        {
            rb.velocity = new Vector2(maxSpeed,rb.velocity.y);
        }
        else if(rb.velocity.x<-maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if(Input.GetButtonUp("Horizontal"))//방향키 뗐을 시x축 움직임 천천히 감소
        {
            rb.velocity= new Vector2(rb.velocity.normalized.x * 0.5f,rb.velocity.y);
        }

        sprRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;//캐릭터 sprite 좌우 뒤집기 결정

        if(Mathf.Abs(rb.velocity.x)<0.2f)//걷기 에니메이션 시작/종료 조건
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        if(Input.GetKeyDown(KeyCode.R))//테스트모드 캐릭터 리셋
        {
            Debug.Log("테스트 모드 : 캐릭터위치 재설정");
            transform.position = new Vector2(0, 0);
            GameManager.GM.health = 100;
        }
        
    }
    private void FixedUpdate()
    {
        if(rb.velocity.y<0)//점프 에니메이션 종료시키기
        {
            Debug.DrawRay(rb.position, Vector2.down, Color.green);
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector2.down, 0.6f, LayerMask.GetMask("Platform"));
            RaycastHit2D rayHit2 = Physics2D.Raycast(rb.position, Vector2.down, 0.6f, LayerMask.GetMask("Monster"));

            if(rayHit.collider!=null || rayHit2.collider!=null)//플랫폼 또는 몬스터가 ray에 걸리면 jump애니메이션 종료
            {
                anim.SetBool("isJumping", false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Monster")
        {
            if(transform.position.y>collision.gameObject.transform.position.y+0.4f )
            {
                OnAttack(collision);
            }
            else if(!hit)
            {
                OnHit(collision.transform);
            }
        }
        else if(collision.collider.name=="Flag")
        {
            StartCoroutine(GameManager.GM.NextStage());
        }
        else if(collision.collider.name== "Obstacles")
        {
            if (!hit)
                OnHit(collision.transform);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Jump")
        {
            anim.SetBool("isJumping", false);
        }
        if(collision.gameObject.tag=="Item")
        {
            if(collision.gameObject.name.Contains("Gold"))
            {
                Destroy(collision.gameObject);
                GameManager.GM.coin++;
            }
            else if (collision.gameObject.name.Contains("Silver"))
            {
                Destroy(collision.gameObject);
                GameManager.GM.coin+=0.3f;
            }
            else if (collision.gameObject.name.Contains("Broze"))
            {
                Destroy(collision.gameObject);
                GameManager.GM.coin+=0.1f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Jump")
        {
            anim.SetBool("isJumping", true);
        }
    }
    private void OnHit(Transform enenmy)//적에게 닿거나 데미지 입었을 경우 모션
    {
        hit = true;
        int backAndForth = rb.position.x > enenmy.position.x ? 1 : -1;//C#에서는 int의 unsigned / signed 구별 없음        
        rb.AddForce(Vector2.right * backAndForth, ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * 7,ForceMode2D.Impulse);
        StartCoroutine(CameraController.cam.ShakeCamera());
        StartCoroutine(OnDamaged());
    }
    IEnumerator OnDamaged()
    {
        int count = 0;
        GameManager.GM.health--;
        anim.SetBool("isJumping", true);
        while(count<5)
        {
            sprRenderer.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.15f);
            sprRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.15f);
            capCollider.enabled = true;
            count++;
        }
        hit = false;
    }   
    void OnAttack(Collision2D collision)//OnColiision2D를 통하여 몬스터에게 데미지를 입혔을 때
    {
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        StartCoroutine(collision.gameObject.GetComponent<MonsterController>().OnDamaged());
        GameManager.GM.stagePoint += 100;        
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

    public void VelocityZero()
    {
        rb.velocity = Vector2.zero;
    }
}
