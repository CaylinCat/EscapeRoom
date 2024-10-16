using UnityEngine;

public class Journal : Puzzle 
{
    public GameObject LeftPage;
    public GameObject RightPage;
    public SpriteRenderer JournalSR;
    public Sprite[] pageSprites;
    private int currentPage = 0;
    
    void Start()
    {
        if (pageSprites.Length > 0)
        {
            JournalSR.sprite = pageSprites[currentPage];
        }
    }

    public void TurnPageFoward() 
    {
        if (currentPage < 1) // change if ever add more interactable pages
        {
            currentPage++;
            JournalSR.sprite = pageSprites[currentPage];
        }
    }

    public void TurnPageBack() 
    {
        if (currentPage > 0) 
        {
            currentPage--;
            JournalSR.sprite = pageSprites[currentPage];
        }
    }
}