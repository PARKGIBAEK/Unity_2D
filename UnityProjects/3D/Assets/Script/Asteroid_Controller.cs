using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Controller : MonoBehaviour
{
    Vector3 vecRotate;
    [SerializeField] GameObject effectObject;
    [SerializeField] float speed = 2.0f;
    private void Awake()
    {
        vecRotate = new Vector3(Random.Range(50.0f, 300.0f), Random.Range(50.0f, 300.0f), Random.Range(50.0f, 300.0f));

        speed = Random.Range(2.0f, 10.0f);
        
        Rigidbody rd = GetComponent<Rigidbody>();
        rd.velocity = -transform.forward * speed;
    }
    private void FixedUpdate()
    {
        transform.Rotate(vecRotate * Time.deltaTime);

        if (transform.position.z <= -6.0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Laser"))
        {
            GameObject obj = Instantiate(effectObject, transform.position, new Quaternion());

            Destroy(obj, 3.0f);//이펙트 삭제 3초 딜레이

            //Destroy(other.gameObject);//레이저 삭제
            Destroy(gameObject);//운석 삭제
        }
    }
}
