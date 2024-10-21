using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hint")]
[System.Serializable]
public class Hint : ScriptableObject
{
    [TextArea]
    public string HintText;
}
