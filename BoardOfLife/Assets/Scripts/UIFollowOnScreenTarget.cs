using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowOnScreenTarget : MonoBehaviour
{
    GameObject target;
    Camera cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 screenPos = cam.WorldToScreenPoint(target.transform.position);
        transform.position = new Vector2(screenPos.x, screenPos.y+75);
    }
}
