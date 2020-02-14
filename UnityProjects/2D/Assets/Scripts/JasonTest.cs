using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// C#은 메모리 정리방식이 직렬이 아니므로 Json 을 사용하려면 시스템을 바꿔야함.
[Serializable]
public class Test
{
    public string name;
}

[Serializable]
public class TextTest
{
    public string name;
    public int age;
    public Test test;
    public int[] counts;
}

[Serializable]
public class TextTest2
{
    public int age;
}
public class JasonTest : MonoBehaviour
{
    private void Awake()
    {
        TextTest test = new TextTest();
        test.name = "홍길동";
        test.age = 100;
        test.test = new Test();//클래스 안의 클래스는 오브젝트안의 오브젝트이다
        test.test.name = "하하하";//오브젝트안의 오브젝트에 내용을 삽입
        test.counts = new int[10];//오브젝트안에 배열을 넣기
        string json = JsonUtility.ToJson(test);//JsonUtility를 이용해서 'test'를 json으로 변환시킴
        //parsing : string형태의 데이터를 가져와서 원하는 데이터 구조에 맞게 집어넣는것
        
        Debug.Log(json);
        TextTest test2 = JsonUtility.FromJson<TextTest>(json);
        Debug.Log(test2.name + ", " + test2.age.ToString());

        TextTest2 test3 = JsonUtility.FromJson<TextTest2>(json);//구조가 꼭 매칭이 되지 않아도 변수값이 같은것이 있으면 그것만 가져 올 수 있다.
        Debug.Log(test3.age);
    }

}
