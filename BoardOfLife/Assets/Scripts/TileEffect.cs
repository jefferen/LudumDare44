using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileEffect : MonoBehaviour
{
    public Sprite lifeSprite, teleport, avoidNextEffect, unknown, hiddenSprite;
    [HideInInspector]
    public bool hidden;
    GameManager gm;
    public Color red, green; // custum text perhaps?
    GameObject teleportPartner, uiTarget, Player;
    public int indexu, lifeCurrency;
    SpriteRenderer spriteRenderer;
    TMPro.TextMeshProUGUI effectValue;

    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UITarget(GameObject UI) // ok now it's getting messy
    {
        uiTarget = UI;
        uiTarget.GetComponent<StaticTileUI>().GiveTarget(transform.position);
        effectValue = uiTarget.GetComponent<TMPro.TextMeshProUGUI>();
        if (hidden)
        {
            effectValue.enabled = false;
        }
        EffectPicker();
    }

    void EffectPicker() // can't move across only up down and from side to side! it limits you very much!! Make teleport with mouse that kan move you in 8 directions
    {
        int index = Random.Range(0, 100);
        effectValue.enabled = true;         //running out of time! It's ugly but it works and the rest of the code does too so no worries at this point
        if (index <= 15)
        {
            // gain life            
            lifeCurrency = Random.Range(1, 5);
            effectValue.color = green;
            effectValue.text = lifeCurrency.ToString();
            if (hidden)
            {
                hiddenSprite = lifeSprite;
                spriteRenderer.sprite = unknown;
                effectValue.enabled = false;
            }
            else
            {
                spriteRenderer.sprite = lifeSprite;
            }
            indexu = 1;
        }
        else if (index > 15 && index <= 85)
        {
            // lose life
            lifeCurrency = Random.Range(-20, -5);
            effectValue.color = red;
            effectValue.text = lifeCurrency.ToString();
            if (hidden)
            {
                hiddenSprite = lifeSprite;
                effectValue.enabled = false;
                spriteRenderer.sprite = unknown;
            }
            else
            {
                spriteRenderer.sprite = lifeSprite;
            }
            indexu = 2;
        }
        else if (index > 85 && index <= 100)
        {
            effectValue.enabled = false;
            if (hidden)
            {
                hiddenSprite = avoidNextEffect;
                spriteRenderer.sprite = unknown;
            }
            else
            {
                spriteRenderer.sprite = avoidNextEffect;
            }
            indexu = 3;
        }
    }

    public void HideUI()
    {
        hidden = true;
        //effectValue.enabled = false;
    }

    public void Teleporter(GameObject v) 
    {
        if (hidden)
        {
            hiddenSprite = teleport;
            spriteRenderer.sprite = unknown;
        }
        Invoke("SetTeleportSprite", 0.07f);
        teleportPartner = v;
    }

    void SetTeleportSprite()
    {
        indexu = 4;
        effectValue.enabled = false;
        spriteRenderer.sprite = teleport;
    }

    void OnTriggerEnter2D(Collider2D other) // godanm make a delegate or an event you fool
    {
        if(other.gameObject.GetComponent<PlayerController>().oneTriggerImunnity)
        {
            Player.GetComponent<PlayerController>().oneTriggerImunnity = false;
            return;
        }
        if (hidden)
        {
            spriteRenderer.sprite = hiddenSprite;
        }
        effectValue.enabled = true;
        switch (indexu)
        {
            case 1:
                Player.GetComponent<PlayerController>().InfluenceHealth(lifeCurrency);
                gm.audioSource.PlayOneShot(gm.lifeGain);
                break;
            case 2:
                Player.GetComponent<PlayerController>().InfluenceHealth(lifeCurrency);
                gm.audioSource.PlayOneShot(gm.lifeLoss);
                break;
            case 3:
                Player.GetComponent<PlayerController>().oneTriggerImunnity = true;
                effectValue.enabled = false;
                gm.audioSource.PlayOneShot(gm.nullEffect);
                break;
            case 4:
                Teleport();
                break;
            default:
                effectValue.enabled = false;
                break;
        }
    }

    void Teleport()
    {
        effectValue.enabled = false;
        gm.audioSource.PlayOneShot(gm.longTeleport);
        teleportPartner.GetComponent<TileEffect>().DisableTrigger();
        Player.transform.position = teleportPartner.transform.position;
    }

    public void DisableTrigger()
    {
        indexu = 0; // genius
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (hidden)
        {
            effectValue.enabled = false;
            spriteRenderer.sprite = unknown;
        }
        EffectPicker();
    }
}
