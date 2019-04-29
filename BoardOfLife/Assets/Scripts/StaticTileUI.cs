using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTileUI : MonoBehaviour
{
    Camera cam;
    Vector2 target;

    void Start() // okay this looks more cheesy than I thought it would but hey, screw you update func!!
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void GiveTarget(Vector2 t)
    {
        target = t;
    }

    void FixedUpdate()
    {
        Vector2 screenPos = cam.WorldToScreenPoint(target);
        transform.position = screenPos;
    }
}
