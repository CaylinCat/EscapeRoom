using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public GameObject Background;
    public GameObject ItemInspect;
    public SpriteRenderer ItemInspectSR;
    private GameObject activePuzzle;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the PuzzleManager singleton!");
            Destroy(this);
        }
    }

    void Start()
    {
        Background.SetActive(false);
    }

    public void ShowPuzzle(GameObject puzzle)
    {
        if(activePuzzle != null) HidePuzzle();

        activePuzzle = puzzle;
        activePuzzle.SetActive(true);
        Background.SetActive(true);
    }

    public void TryHidePuzzle()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f);
        foreach(Collider2D col in collider2Ds)
        {
            if(col.gameObject.tag == "blocker")
            {
                return;
            }
        }

        HidePuzzle();
    }

    public void HidePuzzle()
    {
        if(activePuzzle != null) activePuzzle.SetActive(false);
        Background.SetActive(false);
    }

    public void InspectGenericItem(Item item)
    {
        ItemInspectSR.sprite = item.GetItemSprite();
        ShowPuzzle(ItemInspect);
    }
}
