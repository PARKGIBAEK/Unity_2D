using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Controller : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(123.0f, 234.0f, 55.0f) * Time.deltaTime);
    }
}
