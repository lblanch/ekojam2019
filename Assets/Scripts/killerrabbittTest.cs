using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class killerrabbittTest : MonoBehaviour
{
    float CO2;
    float population;
    float food;
    Land landOfKingdom;
    Energy energyOfKingdrom;
    List<Action> actions = new List<Action>();
    public Text textARG;

    // Start is called before the first frame update
    void Start()
    {
        CO2 = 2000;
        population = 500;
        food = 6000;

        //Initialize land
        landOfKingdom = new Land(30, 20);

        //Initialize energy
        energyOfKingdrom = new Energy(20);

        //add actions to action vector
        Action auxAction = new Action("Increase city land", 0, 20, 0);
        actions.Add(auxAction);
        auxAction = new Action("Add farming land", 10, 0, 0);
        actions.Add(auxAction);
        auxAction = new Action("Turn farming land to forest", 0, 0, 30);
        actions.Add(auxAction);
        auxAction = new Action("Increase farming and city land", 10, 20, 0);
        actions.Add(auxAction);
    }


    void FixedUpdate()
    {
        textARG.text = "City: " + landOfKingdom.city + "%\n" +
                        "Forest: " + landOfKingdom.forest + "%\n" +
                        "Farming: " + landOfKingdom.farming + "%\n" +
                        "CO2: " + CO2 + "%\n" +
                        "population: " + population + "%\n" +
                        "Food: " + food + "%\n";

    }

    public void OnClickButton()
    {
        Debug.Log("button pressed ");
        foreach (Action playerAction in actions)
        {
            Debug.Log("Executing: " + playerAction.description);
            if(playerAction.cityLand >= 0)
            {
                landOfKingdom.IncreaseCityLand(playerAction.cityLand);
            }
            if (playerAction.forestLand >= 0)
            {
                landOfKingdom.IncreaseForest(playerAction.forestLand);
            }
            if (playerAction.farmingLand >= 0)
            {
                landOfKingdom.IncreaseFarming(playerAction.farmingLand);
            }
        }
    }
}
