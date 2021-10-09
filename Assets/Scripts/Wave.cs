using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New wave",menuName ="Wave")]
public class Wave : ScriptableObject
{
  public float[] delays;
  public int[] amounts;
  public float[] spacing;
}
