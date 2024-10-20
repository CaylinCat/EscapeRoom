using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterPuzzle : Puzzle
{
    [Header("Outputs")]
    public Item SealedEnvelope;
    public Item MapKey;
    public TMP_Text NameFeedbackText;

    [Header("Inputs")]
    public GameObject OpenLetterZone;
    public TMP_InputField NameInput;
    public GameObject UseWaxZone;
    public GameObject UseStampZone;
    public GameObject GrabEnvelopeZone;

    [Header("Images")]
    public SpriteRenderer EnvelopeSR;
    public SpriteRenderer LetterSR;
    public Sprite EnvelopeSeparateSprite;
    public Sprite NamedLetterSprite;
    public Sprite ClosedEnvelopeSprite;
    public GameObject WaxUnstamped;
    public GameObject WaxStamped;
    public GameObject MapKeyObject;
    public FMODUnity.StudioEventEmitter WritingSFX;
    public FMODUnity.StudioEventEmitter PaperSFX;

    private string _solutionName = "Elizabeth";
    private bool letterComplete = false;
    private bool mapKeyComplete = false;

    void Awake()
    {
        EnvelopeSR.gameObject.SetActive(true);
        LetterSR.gameObject.SetActive(false);
        WaxUnstamped.SetActive(false);
        WaxStamped.SetActive(false);
        MapKeyObject.SetActive(false);

        OpenLetterZone.SetActive(true);
        NameInput.transform.parent.gameObject.SetActive(false);
        UseWaxZone.SetActive(false);
        UseStampZone.SetActive(false);
        GrabEnvelopeZone.SetActive(false);
    }

    public void OpenLetter()
    {
        EnvelopeSR.sprite = EnvelopeSeparateSprite;
        LetterSR.gameObject.SetActive(true);
        NameInput.transform.parent.gameObject.SetActive(true);
        OpenLetterZone.SetActive(false);
    }

    public void TryName(string nameGuess)
    {
        if(nameGuess.Equals(_solutionName))
        {
            LetterSR.sprite = NamedLetterSprite;
            NameInput.transform.parent.gameObject.SetActive(false);
            WritingSFX.Play();
            StartCoroutine(WaitCloseLetter());
        }
        else
        {
            NameFeedbackText.text = $"I know not of any soul who bears the name {nameGuess}...";
        }
    }

    private IEnumerator WaitCloseLetter()
    {
        yield return new WaitForSeconds(1);
        CloseLetter();
    }

    public void CloseLetter()
    {
        PaperSFX.Play();
        EnvelopeSR.sprite = ClosedEnvelopeSprite;
        LetterSR.gameObject.SetActive(false); 
        UseWaxZone.SetActive(true);
    }

    public void PlaceWax()
    {
        WaxUnstamped.SetActive(true);
        UseWaxZone.SetActive(false);
        UseStampZone.SetActive(true);
    }

    public void StampWax()
    {
        WaxUnstamped.SetActive(false);
        WaxStamped.SetActive(true);
        UseStampZone.SetActive(false);
        GrabEnvelopeZone.SetActive(true);
    }

    public void GrabLetter()
    {
        if(letterComplete) return;

        letterComplete = true;
        MapKeyObject.SetActive(true);
        EnvelopeSR.gameObject.SetActive(false);
        GrabEnvelopeZone.SetActive(false);
        InventoryManager.Instance.AddItem(SealedEnvelope);
        OnComplete();
    }

    public void GrabKey()
    {
        if(mapKeyComplete) return;

        mapKeyComplete = true;
        MapKeyObject.SetActive(false);
        InventoryManager.Instance.AddItem(MapKey);
        gameObject.SetActive(false);
    }
}
