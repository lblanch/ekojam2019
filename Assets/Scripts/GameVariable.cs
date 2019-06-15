using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariable
{
    public string name;
    public string unit;
    public float value;
    public float CO2Emission;
    public float scalingFactor;
    public float maxValue;
    public float changeValue;
    public List<Action> actions;


    public GameVariable(string _name, string _unit, float _value, float _CO2Emission, float _scalingFactor = 1, float _maxValue = 0) 
    {
        name = _name;
        unit = _unit;
        value = _value;
        CO2Emission = _CO2Emission;
        scalingFactor = _scalingFactor;
        maxValue = _maxValue;
        changeValue = _value;

    }

    public float CalculateCO2()
    {
        return value * CO2Emission * scalingFactor;
    }

    public void ChangeValue(float amount, bool checkMax = false)
    {
        /*if(checkMax)
        {
            if ((value+amount) > maxValue) 
            {
                return;
            }
        }*/
        value += amount;
    }

    public void SetChangedValue(float amount, bool checkMax = false)
    {
        changeValue += amount;
    }

    public bool UpdateCurrentValue(bool checkMax = false)
    {
        if (changeValue > 0)
        {
            if (checkMax)
            {
                if(changeValue > maxValue)
                {
                    return false;
                }
                else
                {
                    value = changeValue;
                    changeValue = 0;
                    return true;
                }
            }
            else
            {
                value = changeValue;
                changeValue = 0;
                return true;
            }
        }
        return false;
    }
}