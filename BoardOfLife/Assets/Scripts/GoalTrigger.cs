using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit something");
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>().LevelComplete();
    }
}
