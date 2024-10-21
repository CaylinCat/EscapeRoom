using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedCandlePuzzle : Puzzle
{
    public Item Wax;
    public SpriteRenderer spriteRenderer;
    public Sprite litCandleSprite;
    public GameObject WaxImg;
    public FMODUnity.StudioEventEmitter IgniteSFX;
    private bool complete = false;

    public void GrabWax()
    {
        if(complete) return;

        InventoryManager.Instance.AddItem(Wax);
        WaxImg.SetActive(false);
        complete = true;
        OnComplete();
    }

    public void LightCandle()
    {
        IgniteSFX.Play();
        spriteRenderer.sprite = litCandleSprite;
        WaxImg.SetActive(true);
        Debug.Log("triggered light candle");
    }

    public void Start()
    {
        WaxImg.SetActive(false);
    }
}
