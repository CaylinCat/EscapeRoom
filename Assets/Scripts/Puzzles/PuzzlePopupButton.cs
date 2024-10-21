using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePopupButton : MonoBehaviour
{
    public GameObject Puzzle;
    void OnMouseDown()
    {
        PuzzleManager.Instance.ShowPuzzle(Puzzle);
    }
}
