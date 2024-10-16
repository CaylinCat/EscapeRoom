using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronPuzzle : Puzzle
{
    public Item Goblet;
    public SpriteRenderer CauldronSR;
    public Sprite CompleteCauldronSprite;
    public int AddedIngredients = 0;
    private bool complete = false;

    public List<Item> DEBUG_Ingredients;

    // TODO: delete debug
    void Awake()
    {
        foreach(Item i in DEBUG_Ingredients)
        {
            InventoryManager.Instance.AddItem(i);
        }
    }

    public void AddItem()
    {
        Debug.Log("Added ingredient");
        if(complete) return;

        // TODO: play sound effect?
        ++AddedIngredients;

        if(AddedIngredients == 3)
        {
            InventoryManager.Instance.AddItem(Goblet);
            CauldronSR.sprite = CompleteCauldronSprite;
            complete = true;
            OnComplete();
        }
    }
}
