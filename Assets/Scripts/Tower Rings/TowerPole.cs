using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TowerPole : MonoBehaviour
{
    public List<TowerRing> Rings;
    [SerializeField] private RingSizeID _topRingSize;
    [SerializeField] private List<Transform> _anchors;

    void OnEnable()
    {
        for(int i = 0; i < Rings.Count; ++i)
        {
            Rings[i].transform.position = _anchors[i].position;
        }
    }

    void OnMouseDown() { TryPlaceRing(); }

    public void TryPlaceRing()
    {
        if(TowerPuzzle.Instance.SelectedRing != null)
        {
            if(TowerPuzzle.Instance.SelectedRing.RingSize > _topRingSize)
            {
                TowerPuzzle.Instance.PlaceRing(this);
            }
        }
    }

    public Vector3 GetTopAnchorPosition()
    {
        return _anchors[Rings.Count - 1].position;
    }

    public void UpdateInteractions()
    {
        _topRingSize = CalcTopRingSize();

        if(TowerPuzzle.Instance.SelectedRing != null && TowerPuzzle.Instance.SelectedRing.RingSize > _topRingSize)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else GetComponent<BoxCollider2D>().enabled = false;

        foreach(TowerRing r in Rings)
        {
            r.GetComponent<BoxCollider2D>().enabled = false;
            if(r.RingSize == _topRingSize) r.GetComponent<BoxCollider2D>().enabled = true;
        }
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
