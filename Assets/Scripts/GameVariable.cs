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


    public GameVariable(string _name, string _unit, float _value, float _CO2Emission, float _scalingFactor = 1, float _maxValue = 0) 
    {
        name = _name;
        unit = _unit;
        value = _value;
        CO2Emission = _CO2Emission;
        scalingFactor = _scalingFactor;
        maxValue = _maxValue;

    }

    public float calculateCO2()
    {
        return value * CO2Emission * scalingFactor;
    }

    public void changeValue(float amount, bool checkMax = false)
    {
        if(checkMax)
        {
            if ((value+amount) > maxValue) 
            {
                return;
            }
        }
        value += amount;
    }
}