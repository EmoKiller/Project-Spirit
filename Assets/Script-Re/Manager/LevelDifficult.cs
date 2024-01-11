using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDifficult 
{
    public string type;
    public float value;
    public TypeControlDifficult Type => (TypeControlDifficult)Enum.Parse(typeof(TypeControlDifficult), type);
    public LevelDifficult()
    {

    }
}
