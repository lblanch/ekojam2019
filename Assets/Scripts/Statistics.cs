using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    public Slider EmissionsSlider;
    //private float CO2AmountYearlyInKilotons;
    //private float CO2ChangeSpeedInUI = 1f;

    public Slider PeopleSlider;
    //private int PopulationAmount;
    //private float populationChangeSpeedInUI = 1f;

    public Slider FoodSlider;
    //private int FoodAmount;
    //private float foodChangeSpeedInUI = 2f;


    // Updates slider values abruptly when changes happen.
    void FixedUpdate()
    {
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            //tests what the variables are named and their values: print(var.name + " " + var.value);

            // Change emissions slider value to match current value that is written to screen.
            // Links var and slider by var name manually.
            if (var.name == "CO2")
            {
                EmissionsSlider.value = var.value;
            }
            if (var.name == "Population")
            {
                PeopleSlider.value = var.value;
            }
            if (var.name == "Food")
            {
                FoodSlider.value = var.value;
            }
        }
    }
}
