using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController cam=null;
    private Vector3 originalPos;
    private Quaternion originalRot;
    bool isTraking;
    void Awake()
    {
        if (cam == null)
        {
            cam = this;
        }
        else if (cam != this)
        {
            Destroy(this.gameObject);
            Debug.Log("Camera 오브젝트가 이미 생성되어있으므로, 소멸시킵니다.");
        }

        DontDestroyOnLoad(this.gameObject);
        isTraking = true;
        originalPos = transform.localPosition;//원래 포지션 저장
        originalRot = transform.localRotation;//원래 기울기 저장

        transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, transform.position.z);        
    }    
    void Update()
    {
        if(isTraking)
            transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, transform.position.z);
    }

    public IEnumerator ShakeCamera(float duration = 0.5f, float magnitudePos = 0.1f, float magnitudeRot = 0.1f)
    {
        isTraking = false;
        originalPos = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, transform.position.z);//원래 포지션 저장
        originalRot = transform.rotation;//원래 기울기 저장
        float passTime = 0.0f;
        while (passTime < duration)
        {
            Vector3 shakePos = Random.insideUnitSphere;//1 radius반경범위만큼의 난수를 발생
            
            transform.position += shakePos * magnitudePos;//카메라 난수범위만큼 이동
            if (transform)
            {//연속성이 있는 난수를 발생시키는 PerlinNoise함수를 이용하여 랜덤한 회전값을 받아옴
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0.0f));//바라보는 축이(Z)이므로 이 축을 기준으로 난수를 발생시켜 회전
                transform.rotation = Quaternion.Euler(shakeRot);//Quaternion의 Euler함수를 이용하여 랜덤한 회전값을 적용시킨다.
            }
            passTime += Time.deltaTime;

            yield return null;//다음 프레임으로 넘겨줌
            transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, transform.position.z);
            transform.rotation = originalRot;
        }
        isTraking = true;
    }
}
