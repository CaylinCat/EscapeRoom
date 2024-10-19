using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobletInspector : MonoBehaviour
{
    [Header("Outputs")]
    public Item Coin;
    public Item Bone;
    [Header("Images")]
    public SpriteRenderer GobletSR;
    public Sprite EmptyGobletSprite;
    public GameObject CoinObject;
    public GameObject BoneObject;
    private int _itemsRemaining = 2;

    public void DrinkPotion()
    {
        GobletSR.sprite = EmptyGobletSprite;
        CoinObject.SetActive(true);
        BoneObject.SetActive(false);
    }
    
    public void GrabBone()
    {
        InventoryManager.Instance.AddItem(Bone);
        Destroy(BoneObject);

        if(_itemsRemaining == 0)
        {
            InventoryManager.Instance.RemoveItem();
            PuzzleManager.Instance.HidePuzzle();
        }
    }
}
