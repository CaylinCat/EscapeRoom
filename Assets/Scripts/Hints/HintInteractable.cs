using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintInteractable : MonoBehaviour
{
    public Hint HintRef;
    private bool paidFor;

    void Start() { paidFor = false; }

    void OnEnable()
    {
        // TODO indicate if all hints used up
    }

    void OnMouseDown()
    {
        if(!paidFor)
        {
            if(PuzzleManager.Instance.RemainingHints > 0)
            {
                PuzzleManager.Instance.RemainingHints--;
                paidFor = true;
            }
            else return;
        }

        PuzzleManager.Instance.ShowHint(HintRef);
    }
}
