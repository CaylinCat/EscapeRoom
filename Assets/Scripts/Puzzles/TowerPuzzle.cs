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
    public FMODUnity.StudioEventEmitter PlaceSFX;

    [SerializeField] private TowerRing _selectedRing;
    public TowerRing SelectedRing
    {
        get
        {
            return _selectedRing;
        }
        set
        {
            _selectedRing = value;
            if(value != null) OldPole = value.Pole;
            else OldPole = null;
            UpdatePoles();
        }
    }
    [SerializeField] private TowerPole _oldPole;
    public TowerPole OldPole
    {
        get
        {
            return _oldPole;
        }
        set
        {
            _oldPole = value;
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

    void OnEnable()
    {
        if(SelectedRing != null) SelectedRing.Deselect();
        UpdatePoles();
    }

    public void PlaceRing(TowerPole newPole)
    {
        if(Complete) return;

        // Update pole interactability
        OldPole.Rings.Remove(SelectedRing);
        newPole.Rings.Add(SelectedRing);
        
        // Visually move ring to new pole position
        SelectedRing.Pole = newPole;
        SelectedRing.transform.position = newPole.GetTopAnchorPosition();
        SelectedRing.Deselect();
        PlaceSFX.Play();

        if(CheckSolution())
        {
            Rings[2].gameObject.SetActive(false);
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
