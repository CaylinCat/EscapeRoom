using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public GameObject Background;
    public GameObject ItemInspect;
    public GameObject HintGameObject;
    public TextMeshProUGUI HintText;
    public SpriteRenderer ItemInspectSR;
    public FMODUnity.StudioEventEmitter OnCompleteSFX;
    public int RemainingHints;
    private GameObject activePuzzle;
    private GameObject hintReturnPuzzle;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the PuzzleManager singleton!");
            Destroy(this);
        }
    }

    void Start()
    {
        Background.SetActive(false);
        RemainingHints = 3;
    }

    public void ShowPuzzle(GameObject puzzle)
    {
        if(activePuzzle != null) HidePuzzle();

        activePuzzle = puzzle;
        activePuzzle.SetActive(true);
        Background.SetActive(true);
    }

    public void TryHidePuzzle()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f);
        foreach(Collider2D col in collider2Ds)
        {
            if(col.gameObject.tag == "blocker")
            {
                return;
            }
        }

        HidePuzzle();
    }

    public void HidePuzzle()
    {
        if(activePuzzle != null) activePuzzle.SetActive(false);
        Background.SetActive(false);
    }

    public void InspectGenericItem(Item item)
    {
        ItemInspectSR.sprite = item.GetItemSprite();
        ShowPuzzle(ItemInspect);
    }

    public void PlaySFX()
    {
        OnCompleteSFX.Play();
    }

    public void ShowHint(Hint hint)
    {
        hintReturnPuzzle = activePuzzle;
        HintText.text = hint.HintText;
        ShowPuzzle(HintGameObject);
    }

    public void ReturnFromHint()
    {
        ShowPuzzle(hintReturnPuzzle);
    }
}
