using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public List<GameObject> Rows;
    public List<GameObject> Row1Solution;
    public List<GameObject> Row2Solution;
    public List<GameObject> Row3Solution;
    public List<GameObject> Row4Solution;
    public List<GameObject> Row5Solution;

    private List<List<GameObject>> _solutions = new();

    void Awake()
    {
        _solutions.Add(Row1Solution);
        _solutions.Add(Row2Solution);
        _solutions.Add(Row3Solution);
        _solutions.Add(Row4Solution);
        _solutions.Add(Row5Solution);
    }

    // DEBUG
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) Debug.Log(CheckSolution());
    }

    public void PlacePin(GameObject source)
    {
        source.transform.GetChild(0).gameObject.SetActive(false);
        source.transform.GetChild(1).gameObject.SetActive(true);

        if(CheckSolution())
        {
            Debug.Log("PUZZLE SOLVED SUCCESSFULLY");
        }
    }

    public bool CheckSolution()
    {
        for(int i = 0; i < Rows.Count; ++i)
        {
            foreach(Transform tr in Rows[i].GetComponentInChildren<Transform>())
            {
                if (_solutions[i].Contains(tr.gameObject) ^ tr.GetChild(1).gameObject.activeInHierarchy) return false;
            }
        }
        return true;
    }
}
