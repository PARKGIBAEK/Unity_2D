using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AsteroidSpawn
{
    public float xMin;
    public float xMax;
    public float yPos;
    public float zPos;
}
public class SpawnManager : MonoBehaviour
{
    public GameObject[] asteroids;
    public AsteroidSpawn spawnPosition;

    private void Awake()
    {
        StartCoroutine(OnSpawnAsteroid());//코루틴 실행하는 함수;
    }

    IEnumerator OnSpawnAsteroid()//코루틴으로 무한반복으로 생성
    {
        while (true)
        {
            float time = UnityEngine.Random.Range(1.0f, 3.0f);
            yield return new WaitForSeconds(time);

            int count = asteroids.Length;
            int targetNum = UnityEngine.Random.Range(0, count);

            Vector3 pos = new Vector3(UnityEngine.Random.Range(spawnPosition.xMin, spawnPosition.xMax), spawnPosition.yPos, spawnPosition.zPos);
            Instantiate(asteroids[targetNum], pos, new Quaternion());
        }
    }
    private void Update()
    {
      
    }
}
