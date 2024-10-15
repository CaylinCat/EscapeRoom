using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TowerPole : MonoBehaviour
{
    public RingSizeID TopRingSize;
    public List<TowerRing> Rings;

    void OnMouseDown() { TryPlacePin(); }

    void Awake()
    {
        UpdateInteractions();
    }

    private void TryPlacePin()
    {
        if(TowerPuzzle.Instance.SelectedRing != null)
        {
            if(TowerPuzzle.Instance.SelectedRing.RingSize < TopRingSize)
            {
                TowerPuzzle.Instance.PlaceRing(this);
            }
        }
    }

    public void UpdateInteractions()
    {
        Debug.Log($"{gameObject.name} updating...");
        TopRingSize = CalcTopRingSize();
        if(TopRingSize > RingSizeID.EMPTY)
        {
            Debug.Log($"{gameObject.name} TopRingSize = {TopRingSize}, disabling collider");
            GetComponent<CircleCollider2D>().enabled = false;
        }
        else GetComponent<CircleCollider2D>().enabled = true;
    }

    private RingSizeID CalcTopRingSize()
    {
        if(Rings.Count == 0) return RingSizeID.EMPTY;

        RingSizeID biggestSize = RingSizeID.EMPTY;
        foreach(TowerRing r in Rings)
        {
            if(r.RingSize > biggestSize) biggestSize = r.RingSize;
        }
        return biggestSize;
    }
}
