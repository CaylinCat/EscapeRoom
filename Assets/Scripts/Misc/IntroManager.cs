using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject[] IntroImages;
    public GameObject ContinueButton;
    public GameObject BeginButton;
    private int sequenceIndex;

    void Start() 
    {
        sequenceIndex = 0;
        foreach(GameObject img in IntroImages)
        {
            img.SetActive(false);
        }
        IntroImages[sequenceIndex].SetActive(true);
        ContinueButton.SetActive(true);
        BeginButton.SetActive(false);
    }

    public void AdvanceSequence()
    {
        IntroImages[sequenceIndex].SetActive(false);
        sequenceIndex++;
        if(sequenceIndex == IntroImages.Length - 1)
        {
            ContinueButton.SetActive(false);
            BeginButton.SetActive(true);
        }
        IntroImages[sequenceIndex].SetActive(true);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
