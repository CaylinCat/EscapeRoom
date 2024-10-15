using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : Puzzle
{
    private readonly int[] bricksOrder = { 0, 1, 2, 3, 4, 5, 6 };
    public int nextBrickClicked = 0;
    public SpriteRenderer BrickStart;
    public Sprite BricksEnd;
    public bool bricksComplete = false;

    public void Start() {
        nextBrickClicked = 0;
        bricksComplete = false;
        Debug.Log($"Next expected: {nextBrickClicked}, CorrectOrderSize: {bricksOrder.Length}");
    }

    void Awake()
    {
        ResetPuzzle();
    }

    // Call this method when a brick is clicked
    public void OnEntityClicked(int index)
    {
        Debug.Log($"Clicked index: {index}, Next expected: {nextBrickClicked}, CorrectOrderSize: {bricksOrder.Length}");
        Debug.Log($"Complete?: {bricksComplete}");
        if(bricksComplete) return;

        if (index == bricksOrder[nextBrickClicked])
        {
            nextBrickClicked++;

            if (nextBrickClicked >= bricksOrder.Length) {
                Debug.Log("Passed");
                TriggerEvent();
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

    private void TriggerEvent()
    {
        Debug.Log("Correct order clicked! Event triggered!");
        // TODO: Add event upon trigger
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
