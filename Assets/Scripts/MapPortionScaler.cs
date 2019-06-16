using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changes width of different map portions that have tiled images in them.
public class MapPortionScaler : MonoBehaviour
{
    public RectTransform CityLand, FarmLand, Forest;
    private float mapHeight = 500;

    void FixedUpdate()
    {
        ScaleXMapPortions();
    }

    void ScaleXMapPortions()
    {
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
            {
                //print(varVar.name + " " + varVar.value + " " + varVar.unit + "\n");
                if (varVar.name == "City Land")
                    CityLand.sizeDelta = new Vector2(100 + varVar.value, mapHeight);
                if (varVar.name == "Farming Land")
                    FarmLand.sizeDelta = new Vector2(100 + varVar.value, mapHeight);
                if (varVar.name == "Forest")
                    Forest.sizeDelta = new Vector2(100 + varVar.value, mapHeight);
            }

        }
    }
}
