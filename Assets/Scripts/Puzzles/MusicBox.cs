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
    private GameObject _movePinFrom;

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
        // Deactivate use item zone for given slot
        source.transform.GetChild(0).gameObject.SetActive(false);
        // Activate pin in given slot
        source.transform.GetChild(1).gameObject.SetActive(true);

        // Check if puzzle is solved after each action
        if(CheckSolution())
        {
            Debug.Log("PUZZLE SOLVED SUCCESSFULLY");
        }
    }

    public void MovePinStart(GameObject source)
    {
        
    }

    public bool CheckSolution()
    {
        // Check each row of the box
        for(int i = 0; i < Rows.Count; ++i)
        {
            // Check each pin in the given row
            foreach(Transform tr in Rows[i].GetComponentInChildren<Transform>())
            {
                // If the pin is part of the solution AND is not activated
                // OR the pin is not part of the solution AND is activated,
                // the puzzle is not solved and the loop immediately exits

                // Simplifies to: (pin in solution) XOR (pin activated) --> puzzle failed
                if (_solutions[i].Contains(tr.gameObject) ^ tr.GetChild(1).gameObject.activeInHierarchy) return false;
            }
        }
        // If none of the pins fail, the puzzle is solved
        return true;
    }
}
