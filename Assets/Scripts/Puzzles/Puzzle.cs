using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public void OnComplete()
    {
        CountdownTimer.Instance.AddTimePuzzleComplete(180);
        PuzzleManager.Instance.PlaySFX();
    }
}
