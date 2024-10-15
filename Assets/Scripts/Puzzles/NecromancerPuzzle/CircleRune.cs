using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRune : MonoBehaviour
{
  public int Index;
  public OuterCircle outerCircle;
  
  void OnMouseDown()
  {
    outerCircle.InteractRune(this);
  }
}