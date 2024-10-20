using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobletInspector : MonoBehaviour
{
    [Header("Outputs")]
    public Item Coin;
    public Item Bone;
    [Header("Inputs")]
    public GameObject DrinkPotionZone;
    [Header("Images")]
    public SpriteRenderer GobletSR;
    public Sprite EmptyGobletSprite;
    public GameObject CoinObject;
    public GameObject BoneObject;
    private int _itemsRemaining = 2;

    void Awake()
    {
        CoinObject.SetActive(false);
        BoneObject.SetActive(false);
        DrinkPotionZone.SetActive(true);
    }

    public void DrinkPotion()
    {
        GobletSR.sprite = EmptyGobletSprite;
        DrinkPotionZone.SetActive(false);
        CoinObject.SetActive(true);
        BoneObject.SetActive(true);
    }
    
    public void GrabBone()
    {
        InventoryManager.Instance.AddItem(Bone);
        Destroy(BoneObject);
        --_itemsRemaining;

        if(_itemsRemaining == 0)
        {
            InventoryManager.Instance.RemoveItemByID(ItemID.GOBLET);
            PuzzleManager.Instance.HidePuzzle();
        }
    }
    
    public void GrabCoin()
    {
        InventoryManager.Instance.AddItem(Coin);
        Destroy(CoinObject);
        --_itemsRemaining;

        if(_itemsRemaining == 0)
        {
            InventoryManager.Instance.RemoveItemByID(ItemID.GOBLET);
            PuzzleManager.Instance.HidePuzzle();
        }
    }
}
