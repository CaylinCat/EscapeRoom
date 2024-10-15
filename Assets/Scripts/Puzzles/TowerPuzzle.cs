using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPuzzle : Puzzle
{
    public static TowerPuzzle Instance;

    public Item Ring;
    public SpriteRenderer TowersSR;
    public List<TowerRing> Rings;
    public List<TowerPole> Poles;
    public TowerPole StartPole;

    public TowerRing SelectedRing;
    public TowerPole OldPole;

    private bool complete = false;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the MusicBox singleton!");
            Destroy(this);
        }
    }

    public void PlaceRing(TowerPole newPole)
    {
        if(complete) return;
        
        // Visually move ring to new pole position
        SelectedRing.Pole = newPole;
        SelectedRing.transform.position = newPole.transform.position;
        SelectedRing.Deselect();

        // Update pole interactability
        Instance.OldPole.Rings.Remove(SelectedRing);
        newPole.Rings.Add(SelectedRing);
        foreach(TowerPole p in Poles) p.UpdateInteractions();
        
        SelectedRing = null;

        if(CheckSolution())
        {
            complete = true;
            OnComplete();
        }
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
