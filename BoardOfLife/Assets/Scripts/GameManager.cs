using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text, level;
    public AudioSource audioSource;
    public AudioClip complete, death, lifeGain, lifeLoss, nullEffect, shortTeleport, longTeleport;
    public Sprite unknown;
    public List<GameObject> effectUI = new List<GameObject>();
    public GameObject tileEffect, goalLine, player;
    List<GameObject> currentLvlObjects = new List<GameObject>();
    Vector2 tp;
    int lvl, index;

    void Start()
    {
        lvl = 10;
        StartGame();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void StartGame() // ui button activation  too long function
    {
        int k = lvl - index - 9;
        level.text = k.ToString();
        player.GetComponent<PlayerController>().ResetHealth(); // I'm not caching any thing, am I?
        foreach (var item in currentLvlObjects)
        {
            Destroy(item);
        }
        currentLvlObjects.Clear(); // I guess that will work fine!
        player.transform.position = new Vector2(-8, 0);
        Vector2 tileTran = new Vector2(-6.5f,3.75f);

        if (lvl == 12) // lvl 2
        {
            tp = TeleportAppeareance(true); // x is first tp's position in vector2 and y is the second tp's position vector2
        }
        else if(lvl >= 12)
        {
            tp = TeleportAppeareance(false);           
        }

        for (int i = 0; i < lvl; i++)
        {
            tileTran.x += 1.5f; // move 5 units to the right
            tileTran.y = 3.75f;

            for (int j = 0; j < 6; j++)
            {
                GameObject T = Instantiate(tileEffect, tileTran, Quaternion.identity);
                currentLvlObjects.Add(T);
                tileTran.y -= 1.5f;
            }
        }
        if(tp.x != 0) // you should see the first attempt I made here!!
        {
            currentLvlObjects[Mathf.RoundToInt(tp.x)].GetComponent<TileEffect>().Teleporter(
                currentLvlObjects[Mathf.RoundToInt(tp.y)]);

            currentLvlObjects[Mathf.RoundToInt(tp.y)].GetComponent<TileEffect>().Teleporter(
                currentLvlObjects[Mathf.RoundToInt(tp.x)]);
        }

        GameObject G = Instantiate(goalLine, new Vector3(tileTran.x + 6, 0,0), Quaternion.identity);
        player.GetComponent<PlayerController>().goalIndicator = G.transform.position;
        CheckeredPlane();
        GiveUI_ToTiles();
        currentLvlObjects.Add(G);       
    }

    void CheckeredPlane()
    {
        int index = 0;
        foreach (var item in currentLvlObjects)
        {
            index++;
            if (index % 7 == 0) index++;

            if(index%2 == 0)
            {
                item.GetComponent<SpriteRenderer>().sprite = unknown;
                item.GetComponent<TileEffect>().HideUI();
            }
        }
    }

    void GiveUI_ToTiles()
    {
        int index = 0;
        foreach (var item in currentLvlObjects)
        {
            item.GetComponent<TileEffect>().UITarget(effectUI[index]);
            index++;
        }
    }

    public void LevelComplete()
    {
        audioSource.PlayOneShot(complete);
        if(lvl>= 82) text.text = "Congratulations, you beat the game!";
        lvl += 2; // might need to be increased!! Also first introduce the teleport in lvl 2 at a close range from the startpoint
        // goal effect GZ
        index++;
        Invoke("StartGame", 2);
    }

    public void ResetLevel()
    {
        audioSource.PlayOneShot(death);
        Invoke("StartGame", 2);
    }

    Vector2 TeleportAppeareance(bool makeTp)
    {
        if (makeTp) return new Vector2(16, 31);

        int index = Random.Range(0, lvl);
        if (index >= 12)
        {
            int x = Random.Range(3, lvl - 2);
            return new Vector2(x, x+ Random.Range(12, 50));
        }
        else return new Vector2(0,0);
    }
}
