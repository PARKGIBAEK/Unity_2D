using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Controller : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    public Vector3 laserPosition;
    private void Awake()
    {
        Rigidbody rd = GetComponent<Rigidbody>();
        rd.velocity = transform.forward * speed;
        StartCoroutine(OnLaserDestroy());
    }
    private void Update()
    {
        laserPosition = transform.position;
        if (laserPosition.z > 5)
            Destroy(gameObject);
    }
    //Coroutine이란 큐 형태의 방식으로 동작하는 함수
    IEnumerator OnLaserDestroy()//IEnumerator는 coroutine 함수를 생성
    {
        yield return new WaitForSeconds(3.0f);//이 줄을 경계로 이 함수의 위/아래가 나뉨
        //3초 뒤에 호출
        Destroy(gameObject);
    }
}
