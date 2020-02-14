using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    [SerializeField]
    private Rigidbody myRigid;
    private bool key_w, key_s, key_d, key_a;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            key_w = true;
        else if (Input.GetKeyDown(KeyCode.S))
            key_s = true;
        if (Input.GetKeyDown(KeyCode.D))
            key_d = true;
        else if (Input.GetKeyDown(KeyCode.A))
            key_a = true;

        if (Input.GetKeyUp(KeyCode.W))
            key_w = false;
        else if (Input.GetKeyUp(KeyCode.S))
            key_s = false;
        if (Input.GetKeyUp(KeyCode.D))
            key_d = false;
        else if (Input.GetKeyUp(KeyCode.A))
            key_a = false;


        if (key_w)
        {//velocity :  rigidbody component가 적용된 게임 오브젝트의 속도 위치값을 변화시키는게 아니라 속도를 설정하는 것이므로 키를 땐다고해서 속도가 0으로  바뀌지 않으므로 착각하지 않도록한다.
            myRigid.velocity = new Vector3(0, 0, 1);
        }
        else if(key_s)
        {
            myRigid.velocity = Vector3.back;
        }
        
        if(key_d)
        {
            myRigid.velocity = Vector3.right;
        }
        else if(key_a)
        {
            myRigid.velocity = Vector3.left;
        }
    }
}
