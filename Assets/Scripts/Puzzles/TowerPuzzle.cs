using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPuzzle : Puzzle
{
    public static TowerPuzzle Instance;

    public Item Ring;
    public List<TowerRing> Rings;
    public List<TowerPole> Poles;
    public TowerPole StartPole;

    public TowerRing SelectedRing;
    private TowerPole _oldPole;
    public TowerPole OldPole
    {
        get
        {
            return _oldPole;
        }
        set
        {
            _oldPole = value;
            UpdatePoles();
        }
    }
    public bool Complete = false;

    void Awake()
    {
        Complete = false;
        
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the TowerPuzzle singleton!");
            Destroy(this);
        }

        UpdatePoles();
    }

    public void PlaceRing(TowerPole newPole)
    {
        if(Complete) return;

        // Update pole interactability
        Instance.OldPole.Rings.Remove(SelectedRing);
        newPole.Rings.Add(SelectedRing);
        
        // Visually move ring to new pole position
        SelectedRing.Pole = newPole;
        SelectedRing.transform.position = newPole.GetAnchorPosition();
        SelectedRing.Deselect();
        
        SelectedRing = null;
        OldPole = null;

        if(CheckSolution())
        {
            InventoryManager.Instance.AddItem(Ring);
            Complete = true;
            OnComplete();
        }
    }

    private void UpdatePoles()
    {
        foreach(TowerPole p in Poles) p.UpdateInteractions();
    }

    public bool CheckSolution()
    {
        TowerPole finalPole = Rings[0].Pole;
        if(finalPole == StartPole) return false;

        foreach(TowerRing r in Rings)
        {
            if(r.Pole != finalPole) return false;
        }
        return true;
    }
}
