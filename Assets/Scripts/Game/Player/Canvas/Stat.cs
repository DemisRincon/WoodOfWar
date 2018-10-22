using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField]
    private BarScript bar;
        
    //(en estos casos, el stat será del nivel de vida del personaje)
    //valor maximo del stat
    [SerializeField]
    private float maxVal;

    //Valor actual de stat 
    [SerializeField]
    private float currentVal;

    //Una propiedad para acceder y modificar el valor actual de stat
    public float CurrentValue
    {
        get
        {
            return currentVal;
        }

        set
        {
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            this.maxVal = value;
            bar.MaxValue = maxVal;
        }
    }
    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentValue = currentVal;
    }
}
