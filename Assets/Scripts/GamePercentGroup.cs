using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePercentGroup
{
    public List<GameVariable> variables = new List<GameVariable>();

    public void AddToGroup(GameVariable var)
    {
        variables.Add(var);
    }

    public void ChangeValue(float amount, int sourceId, int destinationId)
    {
        variables[destinationId].ChangeValue(amount);
        variables[sourceId].ChangeValue(-amount);
    }

    public void SetChangedValues(float amount, int sourceId, int destinationId)
    {
        variables[sourceId].SetChangedValue(-amount);
        variables[destinationId].SetChangedValue(amount);
    }

    public float CalculateCO2Total()
    {
        float auxCO2 = 0;

        foreach (GameVariable gameVar in variables)
        {
            auxCO2 += gameVar.CalculateCO2();    
        }
        return auxCO2;
    }

    public float CalculateCO2(int variableId)
    {
        return variables[variableId].CalculateCO2();
    }
}