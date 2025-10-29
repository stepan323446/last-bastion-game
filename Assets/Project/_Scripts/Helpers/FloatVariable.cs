using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Constants/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    public float Value;
}

[Serializable]
public class FloatReference
{
    public bool useConstant = true;
    public float constantValue;
    public FloatVariable variable;

    public float Value
    {
        get
        {
            return useConstant ? constantValue : variable.Value;
        }
        set
        {
            if (useConstant) constantValue = value;
            else variable.Value = value;
        }
    }
}