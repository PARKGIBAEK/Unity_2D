using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprRenderer;
    CapsuleCollider2D capCollider;
    public int nextMove;
    public int nextThink;
    Vector2 backAndForth;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprRenderer = GetComponent<SpriteRenderer>();
        capCollider = GetComponent<CapsuleCollider2D>();
        nextMove = -1;
        Invoke("Think", 5);
    }
        
    void FixedUpdate()
    {
        //Move
        rb.velocity = new Vector2(nextMove, rb.velocity.y);//왼쪽으로 이동
        //Platform Check
        backAndForth.x = rb.position.x + nextMove * 0.3f;
        backAndForth.y= rb.position.y;
        Debug.DrawRay(backAndForth, Vector2.down, new Color(1, 0, 1));
        RaycastHit2D rayHitPlatform = Physics2D.Raycast(backAndForth, Vector2.down, 1, LayerMask.GetMask("Platform"));
        RaycastHit2D rayHitMonster = Physics2D.Raycast(backAndForth, Vector2.right*nextMove, 1, LayerMask.GetMask("Monster"));

        if (rayHitPlatform.collider == null)//전방에 디딜곳이 없으면 방향 전환
        {
            Turn();
        }                    
    }
    void Think()
    {
        //Set move direction
        nextMove = Random.Range(-1, 2);
        
        //Set animation
        anim.SetInteger("moveSpeed", nextMove);

        //Flip Sprite
        if(nextMove!=0)
            sprRenderer.flipX = nextMove == 1;

        //Recursive
        nextThink = Random.Range(3, 6);//Set next acting time
        Invoke("Think", nextThink);        
    }
    void Turn()
    {
        nextMove = -nextMove;
        sprRenderer.flipX = nextMove == 1;
        //CancelInvoke();
        //Invoke("Think",2);
        
    }

    public void OnDamaged()
    {
        //Sprite alpha
        sprRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite flip Y
        sprRenderer.flipY = true;
        //Collider Disable
        capCollider.enabled = false; 
        //Die Effect jump
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //Destroy
        Destroy(gameObject, 5);
        //Invoke("DeActive", 5);
    }    
}

