using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public DirectionID Direction;
    private Canvas _canvas;

    void Awake()
    {
        _canvas = gameObject.GetComponentInChildren<Canvas>();
    }

    public void SetWallActive(bool state)
    {
        _canvas.gameObject.SetActive(state);
    }
}
