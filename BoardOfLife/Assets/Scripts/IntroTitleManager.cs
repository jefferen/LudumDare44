using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroTitleManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI intro, controls;

    void Start()
    {
        intro.enabled = true;
        controls.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
    
    public void ShowControls()
    {
        intro.enabled = false;
        controls.enabled = true;
    }
    public void ShowIntro()
    {
        intro.enabled = true;
        controls.enabled = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
