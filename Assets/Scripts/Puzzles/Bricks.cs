using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : Puzzle
{
    private readonly int[] bricksOrder = { 6, 2, 1, 6, 0, 4, 2, 1, 5, 3 };
    private int nextBrickClicked = 0;
    public SpriteRenderer BrickStart;
    public Sprite BricksEnd;
    private bool bricksComplete = false;
    public Item Mandrake;
    public Item Stamp;
    public GameObject MandrakeClickable;
    public GameObject StampClickable;
    public FMODUnity.StudioEventEmitter SlideSFX;

    public void Start() {
        nextBrickClicked = 0;
        bricksComplete = false;
        MandrakeClickable.SetActive(false);
        StampClickable.SetActive(false);
    }

    void Awake()
    {
        ResetPuzzle();
    }

    // Call this method when a brick is clicked
    public void OnEntityClicked(int index)
    {
        if(bricksComplete) return;
        SlideSFX.Play();

        if (index == bricksOrder[nextBrickClicked])
        {
            nextBrickClicked++;

            if (nextBrickClicked >= bricksOrder.Length) {
                Debug.Log("Passed");
                BrickStart.sprite = BricksEnd;
                bricksComplete = true;
                MandrakeClickable.SetActive(true);
                StampClickable.SetActive(true);
                this.OnComplete();
            }
        }
        else
        {
            Debug.Log("Incorrect order clicked. Resetting.");
            ResetPuzzle();
        }
    }
    
    private void ResetPuzzle()
    {
        nextBrickClicked = 0;
        bricksComplete = false;
    }

    public void GrabMandrake()
    {
        if(bricksComplete) {
            InventoryManager.Instance.AddItem(Mandrake);
            MandrakeClickable.SetActive(false);
        }
    }
    public void GrabStamp()
    {
        if(bricksComplete) {
            InventoryManager.Instance.AddItem(Stamp);
            StampClickable.SetActive(false);
        }
    }
}
