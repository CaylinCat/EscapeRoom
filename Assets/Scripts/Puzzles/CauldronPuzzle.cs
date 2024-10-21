using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronPuzzle : Puzzle
{
    public Item Goblet;
    public Item GobletFilled;
    public SpriteRenderer CauldronSR;
    public Sprite CompleteCauldronSprite;
    public int AddedIngredients = 0;
    public GameObject UseIngredientZone;
    public GameObject UseGobletZone;
    public FMODUnity.StudioEventEmitter AddItemSFX;
    public FMODUnity.StudioEventEmitter GobletSubmergeSFX;
    private bool complete = false;

    void Awake()
    {
        UseIngredientZone.SetActive(true);
        UseGobletZone.SetActive(false);
    }

    public void AddItem()
    {
        if(complete) return;

        AddItemSFX.Play();
        ++AddedIngredients;

        if(AddedIngredients == 3)
        {
            CauldronSR.sprite = CompleteCauldronSprite;
            UseIngredientZone.SetActive(false);
            UseGobletZone.SetActive(true);
            complete = true;
            OnComplete();
        }
    }

    public void FillGoblet()
    {
        InventoryManager.Instance.AddItem(GobletFilled);
        GobletSubmergeSFX.Play();
    }
}
