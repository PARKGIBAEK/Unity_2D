using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]//유니티에서 클래스로 추가된 변수가 변경가능한 메뉴가 된다.
public class pos
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
}
public class Player_Controller2 : MonoBehaviour
{
    Rigidbody rd;
    //SeiralizeField로 지정해둔 이유는 아래쪽 값을 조정할 수 있기 때문
    [SerializeField] float speed = 3.0f;
    [SerializeField] pos playerPos;
    [SerializeField] float ratateValue = 9.0f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform laserPosition;
    public bool spaceKey = false;
    public float attackDelay = 0.0f;
    private void Awake()
    {
        rd = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            spaceKey = true;
        else if (Input.GetKeyUp(KeyCode.Space))
            spaceKey = false;
        if (spaceKey == true)
            attackDelay += Time.deltaTime;
        if(spaceKey/*&&attackDelay>0.15f*/)
        {
            GameObject.Instantiate(laserPrefab, laserPosition.position, laserPosition.rotation);
            attackDelay = 0;
        }

    }
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rd.velocity = new Vector3(h,0.0f, v) * speed;
        rd.position = new Vector3(
            Mathf.Clamp(rd.position.x, playerPos.xMin, playerPos.xMax)
            , rd.position.y
            , Mathf.Clamp(rd.position.z, playerPos.zMin, playerPos.zMax)
            );
        rd.rotation = Quaternion.Euler(0.0f, 0.0f,
            rd.velocity.x * (-ratateValue));
    }
}
