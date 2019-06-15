using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public string description;
    public float farmingLand;
    public float cityLand;
    public float forestLand;
    //int actionId;
    //bool selected;

    public Action(string _description, float _farmingLand, float _cityLand, float _forestLand)
    {
        description = _description;
        farmingLand = _farmingLand;
        cityLand = _cityLand;
        forestLand = _forestLand;
    }
}
