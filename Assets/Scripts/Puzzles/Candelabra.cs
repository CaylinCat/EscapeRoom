using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Candelabra : Puzzle
{
    public Item Gemstone;
    public SpriteRenderer spriteRenderer;
    public Sprite litCandelabraSprite;
    public GameObject GemstoneImg;
    public FMODUnity.StudioEventEmitter IgniteSFX;
    // private bool complete = false;

    public void GrabGemstone()
    {
        // if(complete) return;
        InventoryManager.Instance.AddItem(Gemstone);
        GemstoneImg.SetActive(false);
        // complete = true;
        // OnComplete();
    }

    public void LightCandle()
    {
        IgniteSFX.Play();
        spriteRenderer.sprite = litCandelabraSprite;
        GemstoneImg.SetActive(true);
        OnComplete();
    }

    public void Start()
    {
        GemstoneImg.SetActive(false);
    }
}
