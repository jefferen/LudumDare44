using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public TMPro.TextMeshProUGUI Health, distanceToGoal;
    CircleCollider2D playerCollider;
    GameManager gm;
    [HideInInspector]
    public Vector2 goalIndicator;
    float h, v; 
    public int moveSpeed, health;
    [HideInInspector]
    public bool oneTriggerImunnity = false;
    Camera cam;

    void Awake()
    {
        playerCollider = GetComponent<CircleCollider2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        ResetHealth();
        rb = GetComponent<Rigidbody2D>(); 
    }

    public void ResetHealth()
    {
        playerCollider.enabled = true;
        Health.text = "100";
        health = 100;
    }

    void Update()
    {
        h = v = 0;
        h += Input.GetAxisRaw("Horizontal");
        v += Input.GetAxisRaw("Vertical");
        distanceToGoal.text = Vector2.Distance(transform.position, goalIndicator).ToString("F1");

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Vector2.Distance(transform.position, ray.GetPoint(0)) < 2.05f) // might need adjustment
            {
                gm.audioSource.PlayOneShot(gm.shortTeleport);
                InfluenceHealth(-2);
                transform.position = ray.GetPoint(0);
            }
        }
    }

    public void InfluenceHealth(int value)
    {
        health += value;
        Health.text = health.ToString();
        if(health <= 0)
        {
            playerCollider.enabled = false;
            gm.ResetLevel();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.zero;  // what the frick physics, took me an hour to stop the sliding, and the code doesn't look like the way I remembered it but it works and I'm not gonna bother anymore
        if (h == 0 && v == 0) return;
        rb.AddForce(Vector2.right * h * moveSpeed);
        rb.AddForce(Vector2.up * v * moveSpeed);
        
    }
}
