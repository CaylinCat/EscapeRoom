using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPuzzle : MonoBehaviour
{
    public DialogueSystem DS;

    // Start is called before the first frame update
    void OnEnable()
    {
        DS.Reset();
        DS.AdvanceLine();
    }

    void OnMouseDown() { DS.AdvanceLine(); }

    public void FinishDialogue()
    {
        PuzzleManager.Instance.HidePuzzle();
    }
}
