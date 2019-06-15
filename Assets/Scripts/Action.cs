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
            VariablesHelper.baseVariables[actionVar.Item1].changeValue(actionVar.Item2);
        }

        for (int i=0;  i< groupActions.Count; i++)
        {
            VariablesHelper.groupVariables[groupAmounts[i].Item1].ChangeValue(groupAmounts[i].Item2, groupActions[i].Item1, groupActions[i].Item2);
        }
    }
}
