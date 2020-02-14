using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class textMeshProScr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI tmpro = this.GetComponent<TextMeshProUGUI>();
        tmpro.text = "abcde";
        tmpro.colorGradient = new VertexGradient(Color.red, Color.yellow, Color.green,Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
