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
    private bool candleLit = false; 
    private bool waxCollected = false;

    public void GrabWax()
    {
        if (complete || waxCollected) return;

        InventoryManager.Instance.AddItem(Wax);
        WaxImg.SetActive(false);
        waxCollected = true;
        complete = true;
        Debug.Log("Wax collected");
        OnComplete();
    }

    public void LightCandle()
    {
        if (candleLit) return;

        IgniteSFX.Play();
        spriteRenderer.sprite = litCandleSprite;
        WaxImg.SetActive(true);
        Debug.Log("Candle lit");
        candleLit = true;
    }

    public void Start()
    {
       WaxImg.SetActive(candleLit && !waxCollected);
       Debug.Log("Wax collected: " + waxCollected);
       Debug.Log("Candle lit: " + candleLit);
    }

    public void OnEnable()
    {
        WaxImg.SetActive(candleLit && !waxCollected);
        Debug.Log("enable Wax collected: " + waxCollected);
        Debug.Log("enable Candle lit: " + candleLit);
    }
}
