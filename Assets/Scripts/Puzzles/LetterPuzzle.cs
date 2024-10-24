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
    public GameObject NameLetterCanvas;
    public FMODUnity.StudioEventEmitter WritingSFX;
    public FMODUnity.StudioEventEmitter PaperSFX;
    public FMODUnity.StudioEventEmitter StampSFX;
    public HintInteractable LetterHI;
    public Hint LetterHint2;
    public Hint LetterHint3;

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
        NameLetterCanvas.SetActive(false);
        UseWaxZone.SetActive(false);
        UseStampZone.SetActive(false);
        GrabEnvelopeZone.SetActive(false);
    }

    public void OpenLetter()
    {
        EnvelopeSR.sprite = EnvelopeSeparateSprite;
        LetterSR.gameObject.SetActive(true);
        NameLetterCanvas.SetActive(true);
        OpenLetterZone.SetActive(false);
    }

    public void TryName(string nameGuess)
    {
        if(nameGuess.Equals(_solutionName))
        {
            LetterSR.sprite = NamedLetterSprite;
            NameLetterCanvas.SetActive(false);
            WritingSFX.Play();
            LetterHI.UpdateHint(LetterHint2);
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
        LetterHI.UpdateHint(LetterHint3);
    }

    public void StampWax()
    {
        StampSFX.Play();
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
        MapPuzzle.ObtainedMapKey = true;
        gameObject.SetActive(false);
    }
}
