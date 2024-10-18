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

    private bool complete = false;

    void Awake()
    {
        BowlEmpty.SetActive(true);
        BowlFull.SetActive(false);
        UseBirdseedZone.SetActive(true);
        UseEnvelopeZone.SetActive(false);
        GrabAmuletZone.SetActive(false);

        BirdSR.transform.position = AnchorAway.position;
        BirdSR.gameObject.SetActive(false);
    }

    public void PlaceBirdseed()
    {
        BowlEmpty.SetActive(false);
        BowlFull.SetActive(true);
        UseBirdseedZone.SetActive(false);
        BirdSR.gameObject.SetActive(true);
        StartCoroutine(WaitBirdAction(3.0f, BirdApproachSprite, AnchorLand, 1f, 
                BirdLandSprite, OnBirdArrival, false));
    }

    private void OnBirdArrival(bool hasAmulet)
    {
        if(!hasAmulet)
        {
            UseEnvelopeZone.SetActive(true);
        }
        else
        {
            GrabAmuletZone.SetActive(true);
        }
    }

    public void GiveLetter()
    {
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
