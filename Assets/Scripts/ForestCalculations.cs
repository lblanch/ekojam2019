using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For calculating things related to forests.
public static class ForestCalculations
{
    // 200 tons of co2 sink per hectare. Depends on forest type. from 100 to 500 per hectare.
    static float forestHaEmissionsInCO2Kilotons = -0.2f;

    // 
    /// <summary>
    /// Calculates how big carbon sink the forest is.
    /// </summary>
    /// <param name="forestAmountHa"> Forest size in hectares. </param>
    /// <returns> Forest emissions in kilotons of CO2 equivalent. </returns>
    public static float GetForestCO2EmissionsKtCO2(float forestAmountHa)
    {
        return forestAmountHa * forestHaEmissionsInCO2Kilotons;
    }

}
