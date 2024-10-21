using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintInteractable : MonoBehaviour
{
    public Hint HintRef;
    public SpriteRenderer HintSR;
    private bool paidFor;

    void Start() { paidFor = false; }

    void OnEnable()
    {
        if(PuzzleManager.Instance.RemainingHints <= 0 && !paidFor)
        {
            HintSR.sprite = PuzzleManager.Instance.HintDisabled;
        }
    }

    void OnMouseDown()
    {
        if(!paidFor)
        {
            if(PuzzleManager.Instance.RemainingHints > 0)
            {
                PuzzleManager.Instance.RemainingHints--;
                HintSR.sprite = PuzzleManager.Instance.HintPaid;
                paidFor = true;
            }
            else
            {
                // TODO play sound effect
                return;
            };
        }

        PuzzleManager.Instance.ShowHint(HintRef);
    }

    public void UpdateHint(Hint hint)
    {
        HintRef = hint;
        paidFor = false;
        if(PuzzleManager.Instance.RemainingHints > 0) HintSR.sprite = PuzzleManager.Instance.HintUnpaid;
        else HintSR.sprite = PuzzleManager.Instance.HintDisabled;
    }
}
