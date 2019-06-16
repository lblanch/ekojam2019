using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VariablesHelper 
{
    public static List<GameVariable> baseVariables = new List<GameVariable>();
    public static List<GamePercentGroup> groupVariables = new List<GamePercentGroup>();
    public static List<Action> actions = new List<Action>();
    public static List<int> actionsAppearences = new List<int>();
    public static string gameOver;
}
