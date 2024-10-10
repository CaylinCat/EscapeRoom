using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public List<GameObject> Rows;
    public List<GameObject> Row1Solution;
    public List<GameObject> Row2Solution;

    private List<List<GameObject>> _solutions = new();

    void Awake()
    {
        _solutions.Add(Row1Solution);
        _solutions.Add(Row2Solution);
    }

    // DEBUG
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) Debug.Log(CheckSolution());
    }

    public bool CheckSolution()
    {
        for(int i = 0; i < Rows.Count; ++i)
        {
            foreach(Transform tr in Rows[i].GetComponentInChildren<Transform>())
            {
                if (_solutions[i].Contains(tr.gameObject) ^ tr.GetChild(0).gameObject.activeInHierarchy) return false;
            }
        }
        return true;
    }
}
