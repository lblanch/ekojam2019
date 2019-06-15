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
        if(variables[sourceId].value > amount)
        {
            variables[destinationId].ChangeValue(amount, true);
            variables[sourceId].ChangeValue(-amount, true);
        }
    }

    public Tuple<float, float> GetChangedValues(float amount, int sourceId, int destinationId)
    {
        if (variables[sourceId].value > amount)
        {
            return new Tuple<float, float>(variables[sourceId].GetChangedValue(amount, true), variables[destinationId].GetChangedValue(-amount, true));
        }
        return new Tuple<float, float>(variables[sourceId].value, variables[destinationId].value);
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