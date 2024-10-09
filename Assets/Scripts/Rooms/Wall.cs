using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public DirectionID Direction;
    [SerializeField] private Canvas _canvas;

    public void SetWallActive(bool state)
    {
        _canvas.gameObject.SetActive(state);
    }
}
