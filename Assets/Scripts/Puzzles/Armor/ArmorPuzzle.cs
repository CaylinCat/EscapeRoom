using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPuzzle : Puzzle
{
    public DialogueSystem DS;
    public Dialogue intro, riddle1, riddle2, riddle3, complete;
    public static int progress = 0;
    public static bool hasFirstItem, hasSecondItem, hasThirdItem;

    [SerializeField] private GameObject popupButton, popupButtonMouse;
    [SerializeField] private GameObject cage, tooth;
    private void Awake()
    {
        DS.DialogueSet = intro;
    }


    void OnEnable()
    {
        DS.Reset();
        switch (progress)
        {
            case 1:
                DS.DialogueSet = riddle2;
                popupButtonMouse.SetActive(true);
                tooth.SetActive(true);
                break;

            case 2:
                DS.DialogueSet = riddle3;
                popupButton.SetActive(true);
                Wallman.canPlayPuzzle = true;
                break;

            case 3:
                DS.DialogueSet = complete;
                break;
        }
        DS.AdvanceLine();
        
    }



    void OnMouseDown() { DS.AdvanceLine(); }

    public void FinishDialogue()
    {
        PuzzleManager.Instance.HidePuzzle();
        if (progress == 0)
        {
            DS.DialogueSet = riddle1;
            cage.GetComponent<CagePuzzle>().EnableEyeball();
        }

    }




}
