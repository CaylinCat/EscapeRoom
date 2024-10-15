using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : Puzzle
{
    private readonly int[] bricksOrder = { 0, 1, 2, 3, 4, 5, 6 };
    private int nextBrickClicked = 0;
    public SpriteRenderer BrickStart;
    public Sprite BricksEnd;
    private bool bricksComplete = false;

    public void Start() {
        nextBrickClicked = 0;
        bricksComplete = false;
    }

    void Awake()
    {
        ResetPuzzle();
    }

    // Call this method when a brick is clicked
    public void OnEntityClicked(int index)
    {
        if(bricksComplete) return;

        if (index == bricksOrder[nextBrickClicked])
        {
            nextBrickClicked++;

            if (nextBrickClicked >= bricksOrder.Length) {
                Debug.Log("Passed");
                // TriggerEvent();
                BrickStart.sprite = BricksEnd;
                bricksComplete = true;
                OnComplete();
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
}
