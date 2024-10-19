using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public CountdownTimer Timer;
    public void OnComplete()
    {
        Timer.AddTimePuzzleComplete(180);
    }
}
