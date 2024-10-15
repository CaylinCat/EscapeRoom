using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRune : MonoBehaviour
{
  public int Index;
  public NecromancerCircle necromancerCircle;
  
  void OnMouseDown()
  {
    necromancerCircle.InteractRune(this);
  }
}