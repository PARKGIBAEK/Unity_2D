using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject go_camera;

    Vector3 rotation;

    // Update is called once per frame
    private void Start()
    {
        rotation = this.transform.eulerAngles;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.W))//W키가 눌리면 true가 반환되는 조건문
        {//this는 이 스크립트가 붙어있는 게임 오브젝트
            this.transform.position = this.transform.position + new Vector3(0, 0, 1)*Time.deltaTime;//this의 위치값을 변화시키는 방법
            //또는 this.transorm.Translate(new Vector3(0, 0, 1) * Time.deltaTime);이렇게 직접 움직일수도 있다.            
            rotation += new Vector3(90, 0, 0) * Time.deltaTime;//x축기준 회전 값 변환
            this.transform.rotation = Quaternion.Euler(rotation);//회전 시키기

            this.transform.LookAt(go_camera.transform.position);

            transform.RotateAround(go_camera.transform.position, Vector3.up, 100 * Time.deltaTime);
        }
    }
}
