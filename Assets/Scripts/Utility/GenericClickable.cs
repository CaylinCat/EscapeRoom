using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericClickable : MonoBehaviour
{
    public UnityEvent Event;
    void OnMouseDown() { Event.Invoke(); }
}
