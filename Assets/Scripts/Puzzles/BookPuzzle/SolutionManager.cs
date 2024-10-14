using UnityEngine;

public class SolutionManager : MonoBehaviour
{
    public Transform[] correctOrder;

    public void CheckPuzzleSolved()
    {
        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (correctOrder[i].childCount == 0 || correctOrder[i].GetChild(0).gameObject != correctOrder[i].gameObject)
            {
                Debug.Log("Puzzle is not solved yet.");
                return;
            }
        }

        Debug.Log("Puzzle solved!");
    }
}