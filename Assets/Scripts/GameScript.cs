using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    List<Action> actions = new List<Action>();
    //public Text textARG;

    // Start is called before the first frame update
    void Start()
    {
        GameVariable auxVar;
        GamePercentGroup auxGroup;
        Action auxAction;

        auxVar = new GameVariable("CO2", "kilotons", 2000, 0);
        VariablesHelper.baseVariables.Add(auxVar);
        auxVar = new GameVariable("Population", "persons", 500, 200);
        VariablesHelper.baseVariables.Add(auxVar);
        auxVar = new GameVariable("Food", "kg", 60000, 0);
        VariablesHelper.baseVariables.Add(auxVar);

        auxGroup = new GamePercentGroup();
        auxVar = new GameVariable("City Land", "m2", 30, 400, 1, 100);
        auxGroup.AddToGroup(auxVar);
        auxVar = new GameVariable("Farming Land", "m2", 30, 600, 1, 100);
        auxGroup.AddToGroup(auxVar);
        auxVar = new GameVariable("Forest", "m2", 40, -2000, 1, 100);
        auxGroup.AddToGroup(auxVar);
        VariablesHelper.groupVariables.Add(auxGroup);

        //add actions to action vector
        auxAction = new Action("Increase city land");
        auxAction.AddGroupAction(0, 2, 0, 20);
        actions.Add(auxAction);

        auxAction = new Action("Add farming land");
        auxAction.AddGroupAction(0, 2, 1, 10);
        actions.Add(auxAction);

        auxAction = new Action("Turn farming land to forest");
        auxAction.AddGroupAction(0, 1, 2, 30);
        actions.Add(auxAction);

        auxAction = new Action("Increase farming and city land");
        auxAction.AddGroupAction(0, 2, 1, 40);
        auxAction.AddGroupAction(0, 2, 0, 20);
        actions.Add(auxAction);

        auxAction = new Action("Just randomly increase CO2");
        auxAction.AddVariableAction(0, 4000);
        actions.Add(auxAction);
    }


    void FixedUpdate()
    {
        /*textARG.text = "";
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            textARG.text += var.name + " " + var.value + var.unit + "\n";
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
                textARG.text += varVar.name + " " + varVar.value + varVar.unit + "\n";
        }*/
    }

    /*public void OnClickButton()
    {
        Debug.Log("button pressed ");
        foreach (Action playerAction in actions)
        {
            Debug.Log("Executing: " + playerAction.description);
            playerAction.ExecuteAction();
        }
    }*/
}
