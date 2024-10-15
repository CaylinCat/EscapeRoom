using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpacerUtil : MonoBehaviour
{
    public float Space;
    public bool vertical;

    void OnValidate()
    {
        int count = 0;
        foreach(Transform child in transform)
        {
            if(vertical) child.localPosition = new Vector3(0, Space*count, 0);
            else child.localPosition = new Vector3(Space*count, 0, 0);
            count++;
        }
    }
}
