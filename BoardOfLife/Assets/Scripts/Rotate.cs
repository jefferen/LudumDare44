using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float index = 0;
    void Update()
    {
        index += 0.25f;
        transform.rotation = Quaternion.Euler(0,0, index);
    }
}
