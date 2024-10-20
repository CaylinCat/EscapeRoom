using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBox : Puzzle
{
    public static MusicBox Instance;
    
    public Item Hair;
    public Item NRClue0;
    public List<GameObject> Rows;
    public List<GameObject> Row1Solution;
    public List<GameObject> Row2Solution;
    public List<GameObject> Row3Solution;
    public List<GameObject> Row4Solution;
    public List<GameObject> Row5Solution;

    public MusicBoxPin SelectedPin;
    public MusicBoxSlot OldSlot;
    public FMODUnity.StudioEventEmitter PlacePinSFX;

    private List<List<GameObject>> _solutions = new();
    private bool complete = false;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the MusicBox singleton!");
            Destroy(this);
        }

        _solutions.Add(Row1Solution);
        _solutions.Add(Row2Solution);
        _solutions.Add(Row3Solution);
        _solutions.Add(Row4Solution);
        _solutions.Add(Row5Solution);
    }

    public void PlacePin(MusicBoxSlot newSlot)
    {
        if(complete) return;

        // Visually move pin to new slot position
        SelectedPin.Slot = newSlot;
        SelectedPin.transform.position = newSlot.transform.position;
        SelectedPin.Deselect();
        SelectedPin = null;

        // Deactivate use item zone for given slot
        Instance.OldSlot.IsFilled = false;
        Instance.OldSlot.GetComponent<BoxCollider2D>().enabled = true;

        // Activate pin in given slot
        newSlot.IsFilled = true;
        newSlot.GetComponent<BoxCollider2D>().enabled = false;
        PlacePinSFX.Play();

        // Check if puzzle is solved after each action
        // if(CheckSolution())
        // {
            InventoryManager.Instance.AddItem(Hair);
            InventoryManager.Instance.AddItem(NRClue0);
            complete = true;
            OnComplete();
        // }
    }

    public bool CheckSolution()
    {
        // Check each row of the box
        for(int i = 0; i < Rows.Count; ++i)
        {
            // Check each pin in the given row
            foreach(Transform tr in Rows[i].GetComponentInChildren<Transform>())
            {
                // If the pin is part of the solution AND is not activated
                // OR the pin is not part of the solution AND is activated,
                // the puzzle is not solved and the loop immediately exits

                // Simplifies to: (pin in solution) XOR (pin activated) --> puzzle failed
                if (_solutions[i].Contains(tr.gameObject) ^ tr.GetComponent<MusicBoxSlot>().IsFilled) return false;
            }
        }
        // If none of the pins fail, the puzzle is solved
        return true;
    }
}
