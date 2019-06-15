using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds values, updates UI values and has references to all game loop User Interface data 
/// except player choices or events. Class uses Unity UI but moving to TextMeshPro UI would be easy.
/// </summary>
public class Statistics : MonoBehaviour
{
    /// <summary> Value is shown in UI and is updated to match CO2AmountYearlyInKilotons. Win condition to lower this to 0. About -5k to 50k range.</summary>
    public Slider CO2Slider;
    /// <summary> Calculated from other info regularly. Finland, for example, produces is 47 Megatons of Co2 equivalent.</summary>
    private float CO2AmountYearlyInKilotons;
    /// <summary> Individual UI element change speeds need to be calibrated later to sensible values. </summary>
    private float CO2ChangeSpeedInUI = 1f;

    /// <summary> How many residents there are in the island. Just the population number in the UI. Not the text before that.</summary>
    public Text PopulationText;
    /// <summary> Would fit int better but in the UI it's better as a float when it's changed little by little. </summary>
    public int PopulationAmount;
    private float populationChangeSpeedInUI = 1f;

    /// <summary> UI Element for the food in storage for the population to eat. Just the population number in the UI. Not the text before that. </summary>
    public Text FoodText;
    public int FoodAmount;
    private float foodChangeSpeedInUI = 2f;



    /// <summary>
    /// Increases or decreases user interface values at moderate speed. Done with MoveTowards but can be changed to lerping if wished.
    /// </summary>
    void FixedUpdate()
    {
        // Null check to get rid of editor errors when not all elements are yet placed.
        if (CO2Slider != null)
        {
            // Change value over time for each UI element.
            CO2Slider.value = Mathf.MoveTowards(CO2Slider.value, CO2AmountYearlyInKilotons, CO2ChangeSpeedInUI);

            PopulationText.text = Mathf.MoveTowards(float.Parse(PopulationText.text), PopulationAmount, populationChangeSpeedInUI).ToString("F0");

            FoodText.text = Mathf.MoveTowards(float.Parse(FoodText.text), FoodAmount, foodChangeSpeedInUI).ToString("F0");
        }


    }
}
