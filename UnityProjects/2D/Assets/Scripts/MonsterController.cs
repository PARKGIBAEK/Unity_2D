using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spr;
    Animator anim;
    CapsuleCollider2D cap;
    CircleCollider2D cir;
    PlayerController player;
    public int nextMove;
    public int think;
    public int playerDamage;
    public float JumpPower;
    public int health;
    bool isJumpAttack;
    Vector2 backAndForth;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider2D>();
        cir = GetComponent<CircleCollider2D>();        
        JumpPower = 6f;
        health = 2;
        Think();
    }
    private void Start()
    {
        playerDamage = PlayerController.player.damage;//플레이어 데미지가 업그레이드되면 갱신 할 것
        //playerDamage = GameObject.Find("Player").GetComponent<PlayerController>().damage;        
    }

    private void FixedUpdate()
    {        
        

        backAndForth.x = rb.position.x + nextMove*0.4f;
        backAndForth.y = rb.position.y;
        Debug.DrawRay(backAndForth, Vector2.down, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(backAndForth, Vector2.down, 2.0f, LayerMask.GetMask("Platform"));//바닥 감지(방향전환용)

        RaycastHit2D rayHit2 = Physics2D.Raycast(rb.position, Vector2.down, 0.6f, LayerMask.GetMask("Platform"));//점프 후 바닥감지하여 점프모션 종료
        rb.velocity = new Vector2(nextMove, rb.velocity.y);//이동

        if (rayHit.collider==null && !isJumpAttack && !anim.GetBool("isJumping"))//전방에 바닥이 없으면 방향 전환
        {
            Turn();
        }
        if (anim.GetBool("isJumping") && rayHit2.collider != null)//점프에니메이션 중에 바닥을 감지하면 점프에니메이션 종료
        {
            if (rb.velocity.y < 0.1f)
            {
                anim.SetBool("isJumping", false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !anim.GetBool("isJumping") && !isJumpAttack)
        {
            if (transform.position.x > collision.gameObject.transform.position.x)//몬스터가 플레이어보다 오른쪽에 있으면 방향 전환
            {
                if (nextMove == 1)//왼쪽으로 이동 중이 아니라면 방향전환
                {
                    Turn();
                    if (Vector2.Distance(transform.position, collision.gameObject.transform.position) <= 2.5f)
                    {
                        StartCoroutine(JumpAttack());
                    }
                }
                else if (nextMove == -1)
                {
                    if (Vector2.Distance(transform.position, collision.gameObject.transform.position) <= 2.5f)
                    {
                        StartCoroutine(JumpAttack());
                    }
                }
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)//몬스터가 플레이어보다 왼쪽에 있을 때
            {
                if (nextMove == 1)
                {
                    if (Vector2.Distance(transform.position, collision.gameObject.transform.position) <= 2.5f)
                    {
                        StartCoroutine(JumpAttack());
                    }
                }
                else if (nextMove == -1)
                {
                    Turn();
                    if (Vector2.Distance(transform.position, collision.gameObject.transform.position) <= 2.5f)
                    {
                        StartCoroutine(JumpAttack());
                    }
                }
            }
        }
    }
    public void Think()
    {
        nextMove = Random.Range(-1, 2);
        anim.SetInteger("moveSpeed", nextMove);
        if(nextMove!=0)
            spr.flipX = nextMove == 1;
        think = Random.Range(3, 6);
        Invoke("Think", think);
    }

    public void Turn()
    {
        spr.flipX = !spr.flipX;
        nextMove = -nextMove;
    }
    IEnumerator JumpAttack()
    {
        isJumpAttack = true;
        anim.SetBool("isJumping", true);
        rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        rb.velocity=new Vector2(nextMove * 3,rb.velocity.y);
        yield return new WaitForSeconds(0.7f);
        isJumpAttack = false;        
    }

    public IEnumerator OnDamaged()//몬스터가 데미지를 입었을 때
    {
        if(health<=playerDamage)
        {
            spr.flipY = true;
            spr.color = new Color(1, 1, 1, 0.3f);
            cap.enabled = false;
            cir.enabled = false;
            rb.AddForce(Vector2.up * 3.5f,ForceMode2D.Impulse);
            Destroy(gameObject, 1.0f);
        }
        else
        {
            health-=playerDamage;
            spr.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spr.color = new Color(1, 1, 1, 1);
        }        
    }

}
