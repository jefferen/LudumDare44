using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOnTopOfPlayer : MonoBehaviour
{
    GameObject target;
    Camera cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector2 screenPos = cam.WorldToScreenPoint(target.transform.position);
        transform.position = screenPos;
    }
}
