using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Rigidbody rd;
    [SerializeField] float speed = 10.0f;//[SerializeField]대신 public을 적어도 된다.
    [SerializeField] float Jump_Force = 0.0f;//[SerializeField]대신 public을 적어도 된다.
    [SerializeField] bool isJump = false;
    [SerializeField] bool startFalling = false;
    [SerializeField] bool spaceCheck=false;
    private void Awake()
    {
        rd=GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        spaceCheck = Input.GetKeyDown(KeyCode.Space);
        
        if(spaceCheck && !isJump)
        {
            isJump = true;
            Jump_Force = 5.0f;            
        }
        else if(isJump)
        {
            spaceCheck = false;
            Jump_Force -= 0.2f;
        }
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //float z = Input.GetAxis("Jump");
        rd.AddForce(new Vector3(h, Jump_Force, v)*speed);
    }
    private void OnTriggerEnter(Collider other)//매개변수로 받아온 오브젝트의 Collider box에 충돌하면 아래 명령이 작동
    {
        if(other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") && isJump)
        {
            Jump_Force = 0;
            isJump = false;
        }
    }
}