using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public string description;
    List<Tuple<int,float>> variableActions = new List<Tuple<int,float>>();
    List<Tuple<int, int>> groupActions = new List<Tuple<int, int>>();
    List<Tuple<int,float>> groupAmounts = new List<Tuple<int,float>>();

    public Action(string _description)
    {
        description = _description;
    }

    public void AddVariableAction(int variableId, float amount)
    {
        variableActions.Add(new Tuple<int,float> (variableId, amount));
    }

    public void AddGroupAction(int groupId, int sourceId, int destinationId, float amount)
    {
        groupActions.Add(new Tuple<int, int>(sourceId, destinationId));
        groupAmounts.Add(new Tuple<int, float> (groupId, amount));
    }

    public void ExecuteAction()
    {
        foreach (Tuple<int, float> actionVar in variableActions)
        {
            VariablesHelper.baseVariables[actionVar.Item1].ChangeValue(actionVar.Item2);
        }

        for (int i=0;  i< groupActions.Count; i++)
        {
            VariablesHelper.groupVariables[groupAmounts[i].Item1].ChangeValue(groupAmounts[i].Item2, groupActions[i].Item1, groupActions[i].Item2);
        }
    }

    public string GetActionChanges()
    {
        string returnStr = "";
        Tuple<float, float> aux;

        foreach (Tuple<int, float> actionVar in variableActions)
        {
            returnStr += VariablesHelper.baseVariables[actionVar.Item1].name + ": " + VariablesHelper.baseVariables[actionVar.Item1].GetChangedValue(actionVar.Item2) + "\n";
        }

        for (int i = 0; i < groupActions.Count; i++)
        {
            aux = VariablesHelper.groupVariables[groupAmounts[i].Item1].GetChangedValues(groupAmounts[i].Item2, groupActions[i].Item1, groupActions[i].Item2);
            returnStr += VariablesHelper.groupVariables[groupAmounts[i].Item1].variables[groupActions[i].Item1].name + ": " + aux.Item1 + "\n";
            returnStr += VariablesHelper.groupVariables[groupAmounts[i].Item1].variables[groupActions[i].Item2].name + ": " + aux.Item2 + "\n";

        }
        return returnStr;
    }
}
