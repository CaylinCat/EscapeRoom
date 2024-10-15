using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionManager : MonoBehaviour
{
    public Transform[] bookSlots;
    public GameObject[] correctOrder;

    public void CheckPuzzleSolved()
    {
        for (int i = 0; i < bookSlots.Length; i++)
        {
            if (bookSlots[i].childCount == 0)
            {
                Debug.Log("Puzzle is not solved yet.");
                return;
            }

            GameObject bookInSlot = bookSlots[i].GetChild(0).gameObject;
            if (bookInSlot != correctOrder[i])
            {
                Debug.Log("Puzzle is not solved yet.");
                return;
            }
        }

        Debug.Log("Puzzle solved!");
    }
}