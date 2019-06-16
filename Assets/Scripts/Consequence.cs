using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consequence
{
    public bool isGameOver;
    public List<Action> actionsArgs;

    public Consequence(bool _isGameOver = false)
    {
        isGameOver = _isGameOver;
        actionsArgs = new List<Action>();
    }

    public void AddConsequenceActions(Action newAction)
    {
        actionsArgs.Add(newAction);
    }

    public string ExecuteConsequenceActions()
    {
        string resultStr = "";

        foreach (Action aux in actionsArgs)
        { 
            aux.ExecuteAction();
            resultStr += aux.ActionsToString();
        }

        return resultStr;
    }

}
