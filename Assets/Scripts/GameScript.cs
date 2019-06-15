using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{

    public GameObject ActionButtonPrefab;
    public Text textAllVariables;
    public Text textAllChanges;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();

        //create one button per each action
        //TODO create only fixed amount of buttons, according to what fits in screen
        //TODO choose what actions we show
        for (int i=0; i<VariablesHelper.actions.Count; i++)
        {
            GameObject newButton = GameObject.Instantiate(ActionButtonPrefab);
            newButton.transform.SetParent(GameObject.Find("Choices").transform, false);
            newButton.name = "action_"+i;
            //TODO Store the actionId somewhere in the button object, if possible
            //TODO Adjust button height to fit all text
            newButton.GetComponentInChildren<Text>().text = VariablesHelper.actions[i].description + "\n" + VariablesHelper.actions[i].ActionsToString();

            //TODO find a way to programatically get the height of the button
            //newButton.GetComponent<RectTransform>().rect.size.y doesn't seem to take the objects scaling
            newButton.transform.position = newButton.transform.position + new Vector3(0, i * 60, 0);
        }
    }


    void FixedUpdate()
    {
        textAllVariables.text = "CURRENT STATS\n\n";
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            textAllVariables.text += "- " + var.name + " " + var.value + " " + var.unit + "\n";
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
                textAllVariables.text += "- " + varVar.name + " " + varVar.value + " " + varVar.unit + "\n";
        }

        textAllChanges.text = "AFTER CHANGES STATS\n\n";
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            textAllChanges.text += "- " + var.name + " " + var.changeValue + " " +  var.unit + "\n";
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
                textAllChanges.text += "- " + varVar.name + " " + varVar.changeValue + " " + varVar.unit + "\n";
        }
    }

    public void ApplySelectedActions()
    {
        int actionId;

        //This way goes through all toggled buttons and executes their actions in order
        /*
        //TODO if changedValue is negative or not acceptable, show message and cancel this action
        GameObject.Find("Choices").GetComponentInChildren<Button>(false);
        foreach (Button actionButton in GameObject.Find("Choices").GetComponentsInChildren<Button>(false))
        {
            if(actionButton.GetComponentInChildren<ToggleButton>().IsToggled())
            {
                actionId = int.Parse(actionButton.transform.name.Substring(7));
                VariablesHelper.actions[actionId].ExecuteAction();
            }
        }*/

        //TODO THIS BELOW IS WRONG: IF THE VARIABLE WITH THE WRONG AMOUNT IS THE LAST ONE, THE PREVIOUS CHANGES ARE NOT UNDONE!
        //In this way we go through all our variables and simply take the already calculated value as current
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            if(!var.UpdateCurrentValue())
            {
                //TODO show this message to the user in a pop up window or something
                Debug.Log(var.name + " With the current combination of actions, some of the stats have a wrong value. Please choose differently!");
                return;
            }
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
            {
                if (!varVar.UpdateCurrentValue(true))
                {
                    //TODO show this message to the user in a pop up window or something
                    Debug.Log(varVar.name + "With the current combination of actions, some of the stats have a wrong value. Please choose differently!");
                    return;
                }

            }
        }

    }

    void LoadData()
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
        VariablesHelper.actions.Add(auxAction);

        auxAction = new Action("Add farming land");
        auxAction.AddGroupAction(0, 2, 1, 10);
        VariablesHelper.actions.Add(auxAction);

        auxAction = new Action("Turn farming land to forest");
        auxAction.AddGroupAction(0, 1, 2, 30);
        VariablesHelper.actions.Add(auxAction);

        auxAction = new Action("Increase farming and city land");
        auxAction.AddGroupAction(0, 2, 1, 40);
        auxAction.AddGroupAction(0, 2, 0, 20);
        VariablesHelper.actions.Add(auxAction);

        auxAction = new Action("Just randomly increase CO2");
        auxAction.AddVariableAction(0, 4000);
        VariablesHelper.actions.Add(auxAction);
    }
}
