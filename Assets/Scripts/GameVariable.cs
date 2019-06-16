using System;
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
    public float minValue;
    public float maxValue;
    public Consequence minConsequence;
    public Consequence maxConsequence;
    public float changeValue;
    public List<Tuple<int, float>> actionsArgs;

    public GameVariable(string _name, string _unit, float _value, float _CO2Emission, float _maxValue, float _minValue, float _scalingFactor = 1) 
    {
        name = _name;
        unit = _unit;
        value = _value;
        CO2Emission = _CO2Emission;
        maxValue = _maxValue;
        minValue = _minValue;
        scalingFactor = _scalingFactor;
        changeValue = _value;
        actionsArgs = new List<Tuple<int, float>>();
        minConsequence = new Consequence();
        maxConsequence = new Consequence();
    }

    public void AddAction(int destinationVar, float multiplyingFactor)
    {
        actionsArgs.Add(new Tuple<int, float> (destinationVar,multiplyingFactor));
    }

    //executes the actions and returns a string with what has been done
    public string ExecuteVarActions()
    {
        string resultStr = "";
        Action aux;
        foreach(Tuple<int, float> tupleInfo in actionsArgs)
        {
            aux = new Action(name + " action");
            aux.AddVariableAction(tupleInfo.Item1, (tupleInfo.Item2 * value));
            aux.ExecuteAction();
            resultStr += aux.ActionsToString();
        }

        return resultStr;
    }

    public float CalculateCO2()
    {
        return value * CO2Emission * scalingFactor;
    }

    public void ChangeValue(float amount, float amountChange = 0)
    {
        value += amount;
        changeValue = value;
    }

    public void SetChangedValue(float amount)
    {
        changeValue += amount;
    }

    public void SetChangedValueAsCurrent()
    {
        value = changeValue;
        changeValue = value;
    }

    public bool IsValueOK()
    {
        if (changeValue < minValue)
        {
            return false;
        }
        else
        {
            if (changeValue > maxValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}