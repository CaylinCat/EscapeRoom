using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueElement
{
    [TextArea()]
    public string Text;
    public Sprite Speaker;
}

[System.Serializable]
[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<DialogueElement> Lines;
}
