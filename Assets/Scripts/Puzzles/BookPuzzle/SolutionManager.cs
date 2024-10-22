using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionManager : Puzzle
{
    public Transform[] bookSlots;
    public GameObject[] correctOrder; // Symbols
    public GameObject[] correctOrder2; // Alphabetical authors
    public GameObject[] correctOrder3; // Alphabetical titles
    public GameObject moon1;
    public GameObject moon2;
    public GameObject moon3;
    public Item mirror;
    public Item lizzardLeg;
    public Item bricksClue;
    public HintInteractable BookshelfHI;
    public Hint BookshelfHint2; // Authors hint
    public Hint BookshelfHint3; // Titles hint
    bool c = false;
    bool c2 = false;
    bool c3 = false;
    int completedCount = 0;

    void Start() 
    {
        moon1.SetActive(false);
        moon2.SetActive(false);
        moon3.SetActive(false);
    }

    public void CheckPuzzleSolved()
    {
        if ((IsPuzzleSolved(correctOrder) || IsPuzzleSolvedReverse(correctOrder)) && !c)
        {
            c = true;
            completedCount++;
            starUpdate();
            if(!c2) BookshelfHI.UpdateHint(BookshelfHint2);
            else if(!c3) BookshelfHI.UpdateHint(BookshelfHint3);
        }
        else if ((IsPuzzleSolved(correctOrder2) || IsPuzzleSolvedReverse(correctOrder2)) && !c2)
        {
            c2 = true;
            completedCount++;
            starUpdate();
            if(c && !c3) BookshelfHI.UpdateHint(BookshelfHint3);
        }
        else if ((IsPuzzleSolved(correctOrder3) || IsPuzzleSolvedReverse(correctOrder3)) && !c3)
        {
            c3 = true;
            completedCount++;
            starUpdate();
            if(c && !c2) BookshelfHI.UpdateHint(BookshelfHint2);
        }
    }

    private void starUpdate() 
    {
        if (completedCount == 1) {
            moon1.SetActive(true);
            InventoryManager.Instance.AddItem(mirror);
        } else if (completedCount == 2) {
            moon2.SetActive(true);
            InventoryManager.Instance.AddItem(lizzardLeg);
        } else if (completedCount == 3) {
            moon3.SetActive(true);
            InventoryManager.Instance.AddItem(bricksClue);
        }
    }

    private bool IsPuzzleSolved(GameObject[] order)
    {
        for (int i = 0; i < bookSlots.Length; i++)
        {
            if (bookSlots[i].childCount == 0)
            {
                return false;
            }

            GameObject bookInSlot = bookSlots[i].GetChild(0).gameObject;
            if (bookInSlot != order[i])
            {
                return false;
            }
        }
        return true;
    }

    private bool IsPuzzleSolvedReverse(GameObject[] order)
    {
        for (int i = 0; i < bookSlots.Length; i++)
        {
            if (bookSlots[i].childCount == 0)
            {
                return false;
            }

            GameObject bookInSlot = bookSlots[i].GetChild(0).gameObject;
            if (bookInSlot != order[order.Length - 1 - i])
            {
                return false;
            }
        }
        return true;
    }  
}