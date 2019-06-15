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
            variables[destinationId].changeValue(amount, true);
            variables[sourceId].changeValue(-amount, true);
        }
    }

    public float CalculateCO2Total()
    {
        float auxCO2 = 0;

        foreach (GameVariable gameVar in variables)
        {
            auxCO2 += gameVar.calculateCO2();    
        }
        return auxCO2;
    }

    public float CalculateCO2(int variableId)
    {
        return variables[variableId].calculateCO2();
    }
}