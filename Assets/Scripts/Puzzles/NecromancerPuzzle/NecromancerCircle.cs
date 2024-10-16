using System.Collections.Generic;
using UnityEngine;

public class NecromancerCircle : Puzzle
{
    
    public InnerCircle innerCircle;
    public OuterCircle outerCircle;
    private bool isInnerCircleComplete = false;
    private bool isOuterCircleComplete = false;
    void Start()
    {
        if (innerCircle != null)
        {
            innerCircle.OnInnerCircleComplete += HandleInnerCircleComplete;
        }

        if (outerCircle != null)
        {
            outerCircle.OnOuterCircleComplete += HandleOuterCircleComplete;
        }
    }

    private void HandleInnerCircleComplete()
    {
        isInnerCircleComplete = true;
        CheckPuzzleCompletion();
    }

    private void HandleOuterCircleComplete()
    {
        isOuterCircleComplete = true;
        CheckPuzzleCompletion();
    }

    private void CheckPuzzleCompletion()
    {
        if (isInnerCircleComplete && isOuterCircleComplete)
        {
            Debug.Log("Your Soul Is Merged!!!!");
            OnComplete();
        }
    }

   

}

