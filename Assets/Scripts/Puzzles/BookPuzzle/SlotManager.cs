using UnityEngine;
using System.Collections.Generic;

public class SlotManager : MonoBehaviour
{
    public List<Transform> bookSlots;
    public List<GameObject> books;
    public FMODUnity.StudioEventEmitter BookSFX;
    private SolutionManager solutionManager;
    void Start()
    {
        solutionManager = FindObjectOfType<SolutionManager>(); 
        ShuffleBooks();
        PlaceBooksInSlots();
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     foreach (Transform slot in bookSlots)
    //     {
    //         Gizmos.DrawWireCube(slot.position, new Vector3(1, 1, 0.1f));
    //     }
    // }

    public void ShuffleBooks()
    {
        for (int i = 0; i < books.Count; i++)
        {
            GameObject temp = books[i];
            int randomIndex = Random.Range(i, books.Count);
            books[i] = books[randomIndex];
            books[randomIndex] = temp;
        }
    }

    public void PlaceBooksInSlots()
    {
        for (int i = 0; i < books.Count; i++)
        {
            if (i < bookSlots.Count)
            {
                books[i].transform.position = bookSlots[i].position;
                books[i].transform.SetParent(bookSlots[i]);
                books[i].transform.localPosition = Vector3.zero;
            }
        }
    }

    public void MoveBook(GameObject draggedBook, int targetIndex)
    {
        if (targetIndex >= 0 && targetIndex < bookSlots.Count)
        {
            BookSFX.Play();
            books.Remove(draggedBook);
            books.Insert(targetIndex, draggedBook);
            PlaceBooksInSlots();
            solutionManager.CheckPuzzleSolved();
        }
    }

    public int GetClosestSlotIndex(Vector3 bookPosition)
    {
        float closestDistance = Mathf.Infinity;
        int closestIndex = -1;

        for (int i = 0; i < bookSlots.Count; i++)
        {
            float distance = Vector3.Distance(bookPosition, bookSlots[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}

