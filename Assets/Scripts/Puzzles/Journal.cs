using UnityEngine;

public class Journal : Puzzle 
{
    public GameObject LeftPage;
    public GameObject RightPage;
    public SpriteRenderer JournalSR;
    public Sprite[] pageSprites;
    public FMODUnity.StudioEventEmitter PageTurnSFX;
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
        if (currentPage < 2) // change if ever add more interactable pages
        {
            PageTurnSFX.Play();
            currentPage++;
            JournalSR.sprite = pageSprites[currentPage];
        }
    }

    public void TurnPageBack() 
    {
        if (currentPage > 0) 
        {
            PageTurnSFX.Play();
            currentPage--;
            JournalSR.sprite = pageSprites[currentPage];
        }
    }
}