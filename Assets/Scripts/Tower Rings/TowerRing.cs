using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum RingSizeID
{
    EMPTY,
    LARGE,
    MEDIUM,
    SMALL
}

public class TowerRing : MonoBehaviour {
    public TowerPole Pole;
    public RingSizeID RingSize;
    public SpriteRenderer RingSR;

    public void OnMouseDown()
    {
        SelectThis();
        TowerPuzzle.Instance.OldPole = Pole;
    }

    public void OnMouseDrag()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        TowerPuzzle.Instance.OldPole = Pole;
    }

    public void OnMouseUp()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // TODO Check if over rings
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos, 0.01f);
        foreach(Collider2D collider in colliders)
        {
            TowerPole pole = collider.GetComponent<TowerPole>();
            if(pole != null)
            {
                pole.TryPlaceRing();
                break;
            }
        }

        if(Pole != null) transform.position = Pole.transform.position;
        else transform.localPosition = Vector3.zero;
    }

    private void SelectThis()
    {
        if(TowerPuzzle.Instance.SelectedRing != null)
        {
            TowerPuzzle.Instance.SelectedRing.Deselect();
        }
        TowerPuzzle.Instance.SelectedRing = this;
        RingSR.color = Color.white * 0.7f;
    }

    public void Deselect()
    {
        // DEBUG, change back to Color.white
        RingSR.color = Color.red;
    }
}
