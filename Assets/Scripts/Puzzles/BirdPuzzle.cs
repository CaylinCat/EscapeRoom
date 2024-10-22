using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class BirdPuzzle : Puzzle
{
    [Header("Outputs")]
    public Item Amulet;
    [Header("Inputs")]
    public GameObject UseBirdseedZone;
    public GameObject UseEnvelopeZone;
    public GameObject GrabAmuletZone;
    [Header("Images")]
    public GameObject BowlEmpty;
    public GameObject BowlFull;
    public SpriteRenderer BirdSR;
    public Sprite BirdApproachSprite;
    public Sprite BirdLandSprite;
    public Sprite BirdLandLetterSprite;
    public Sprite BirdAwayLetterSprite;
    public Sprite BirdApproachAmuletSprite;
    public Sprite BirdLandAmuletSprite;
    public Transform AnchorAway;
    public Transform AnchorLand;
    public FMODUnity.StudioEventEmitter ChirpingSFX;
    public FMODUnity.StudioEventEmitter FlappingSFX;
    public FMODUnity.StudioEventEmitter SeedsSFX;
    public HintInteractable BirdHI;
    public Hint BirdHint2;

    private bool complete = false;
    private int _state = 0;

    void Awake()
    {
        BowlEmpty.SetActive(true);
        BowlFull.SetActive(false);
        UseBirdseedZone.SetActive(true);
        UseEnvelopeZone.SetActive(false);
        GrabAmuletZone.SetActive(false);

        BirdSR.transform.position = AnchorAway.position;
        BirdSR.gameObject.SetActive(false);

        _state = 0;
    }

    void OnEnable()
    {
        switch(_state)
        {
            case 1:
                BirdSR.transform.position = AnchorLand.position;
                BirdSR.sprite = BirdLandSprite;
                UseEnvelopeZone.SetActive(true);
                break;
            case 2:
                BirdSR.transform.position = AnchorLand.position;
                BirdSR.sprite = BirdLandAmuletSprite;
                GrabAmuletZone.SetActive(true);
                break;
            case 3:
                BirdSR.transform.position = AnchorLand.position;
                BirdSR.sprite = BirdLandSprite;
                break;
            default:
                break;
        }
    }

    public void PlaceBirdseed()
    {
        SeedsSFX.Play();
        _state = 1;
        BowlEmpty.SetActive(false);
        BowlFull.SetActive(true);
        UseBirdseedZone.SetActive(false);
        BirdSR.gameObject.SetActive(true);
        BirdHI.UpdateHint(BirdHint2);
        StartCoroutine(WaitBirdAction(3.0f, BirdApproachSprite, AnchorLand, 1f, 
                BirdLandSprite, OnBirdArrival, false));
    }

    private void OnBirdArrival(bool hasAmulet)
    {
        if(!hasAmulet)
        {
            UseEnvelopeZone.SetActive(true);
            ChirpingSFX.Play();
        }
        else
        {
            GrabAmuletZone.SetActive(true);
        }
    }

    public void GiveLetter()
    {
        _state = 2;
        BirdSR.sprite = BirdLandLetterSprite;
        UseEnvelopeZone.SetActive(false);
        StartCoroutine(WaitBirdAction(1.5f, BirdAwayLetterSprite, AnchorAway, 1.5f, 
                BirdApproachAmuletSprite, BirdReturnWithAmulet, true));
    }

    private void BirdReturnWithAmulet(bool hasAmulet)
    {
        StartCoroutine(WaitBirdAction(5.0f, BirdApproachAmuletSprite, AnchorLand, 1f, 
                BirdLandAmuletSprite, OnBirdArrival, hasAmulet));
    }

    public void GrabAmulet()
    {
        _state = 3;
        if(complete) return;

        complete = true;
        BirdSR.sprite = BirdLandSprite;
        GrabAmuletZone.SetActive(false);
        InventoryManager.Instance.AddItem(Amulet);
        OnComplete();
    }

    public IEnumerator WaitBirdAction(float waitTime, Sprite fromSprite, Transform target, float moveTime, 
            Sprite toSprite, Action<bool> callback, bool hasAmulet)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = BirdSR.transform.position;

        yield return new WaitForSeconds(waitTime);

        FlappingSFX.Play();
        BirdSR.sprite = fromSprite;
        while(BirdSR.transform.position != target.position)
        {
            BirdSR.transform.position = Vector3.Lerp(startPosition, target.position, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        BirdSR.sprite = toSprite;
        callback?.Invoke(hasAmulet);
    }
}
