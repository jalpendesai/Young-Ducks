using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
   [SerializeField]
    private int baseValue;
    public int GetValue(){
        return baseValue;
    }
}
