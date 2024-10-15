using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : Puzzle
{
    private List<int> correctOrder;
    private int nextClick;
    public SpriteRenderer BrickStart;
    public Sprite BricksComplete;
    public bool complete = false;

    public void Start() {
        correctOrder = new List<int> { 0, 1, 2, 3, 4, 5, 6 };
        nextClick = 0;
        Debug.Log($"Next expected: {nextClick}, CorrectOrderSize: {correctOrder.Count}");
    }

    // Call this method when a brick is clicked
    public void OnEntityClicked(int index)
    {
        Debug.Log($"Clicked index: {index}, Next expected: {nextClick}, CorrectOrderSize: {correctOrder.Count}");
        Debug.Log($"Complete?: {complete}");
        if(complete) return;

        if (index == correctOrder[nextClick])
        {
            nextClick++;

            if (nextClick >= correctOrder.Count) {
                Debug.Log("Passed");
                TriggerEvent();
                BrickStart.sprite = BricksComplete;
                //complete = true;
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
        nextClick = 0;
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
