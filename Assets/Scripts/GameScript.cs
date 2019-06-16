using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    const int ACTIONS_PER_CYCLE = 3;
    const int CYCLE_AMOUNT = 5;

    public GameObject ActionButtonPrefab;
    public Text textAllVariables;
    public Text textAllChanges;
    private bool varValuesAreOk;

    // Start is called before the first frame update
    void Start()
    {
        int maxActions = ACTIONS_PER_CYCLE;

        LoadData();

        if (maxActions > VariablesHelper.actions.Count)
        {
            maxActions = VariablesHelper.actions.Count;
        }

        GenerateRandomActionButtons(maxActions);
    }


    void FixedUpdate()
    {
        bool auxVarValues = true;

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
            if (!var.IsValueOK())
            {
                auxVarValues = false;
            }
            textAllChanges.text += "- " + var.name + " " + var.changeValue + " " +  var.unit + "\n";
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
            {
                if (!varVar.IsValueOK())
                {
                    auxVarValues = false;
                }
                textAllChanges.text += "- " + varVar.name + " " + varVar.changeValue + " " + varVar.unit + "\n";
            }
        }

        varValuesAreOk = auxVarValues;
        textAllChanges.text += "\n values ok? " + varValuesAreOk;
    }

    public void ApplySelectedActions()
    {
        int actionId;

        if(!varValuesAreOk)
        {
            //TODO show this message to the user in a pop up window or something
            Debug.Log(" With the current combination of actions, some of the stats have a wrong value. Please choose differently!");
            return;
        }
       
        //execute all actions
        //TODO delete the buttons already here?
        GameObject.Find("Choices").GetComponentInChildren<Button>(false);
        foreach (Button actionButton in GameObject.Find("Choices").GetComponentsInChildren<Button>(false))
        {
            if (actionButton.GetComponentInChildren<ToggleButton>().IsToggled())
            {
                actionId = int.Parse(actionButton.transform.name.Substring(7));
                VariablesHelper.actions[actionId].ExecuteAction();
            }
        }

        //TODO execute all actions of our variables


        //TODO check end game conditions

        //TODO delete current action buttons and generate new ones

    }

    void GenerateRandomActionButtons(int maxActions)
    {
        int auxIndex = 0;
        //create one button per each action, randomly selected
        for (int i = 0; i < maxActions; i++)
        {
            auxIndex = Random.Range(0, maxActions - 1);

            GameObject newButton = GameObject.Instantiate(ActionButtonPrefab);
            newButton.transform.SetParent(GameObject.Find("ChoiceLayout").transform, false);
            newButton.name = "action_" + auxIndex;
            //TODO Store the actionId somewhere in the button object, if possible
            //TODO Adjust button height to fit all text
            newButton.GetComponentInChildren<Text>().text = VariablesHelper.actions[auxIndex].description + "\n" + VariablesHelper.actions[auxIndex].ActionsToString();

            //TODO find a way to programatically get the height of the button
            //newButton.GetComponent<RectTransform>().rect.size.y doesn't seem to take the objects scaling
            //newButton.transform.position = newButton.transform.position + new Vector3(0, 15, 0) + new Vector3(0, i * -65, 0);
        }
    }

    void LoadData()
    {
        GameVariable auxVar;
        GamePercentGroup auxGroup;
        Action auxAction;

        //Indexes for variables
        //CO2 -> 0
        //population -> 1
        //food -> 2
        //happiness -> 3

        auxVar = new GameVariable("CO2", "kilotons", 2000, 0, 500000, -500000);
        VariablesHelper.baseVariables.Add(auxVar);
        auxVar = new GameVariable("Population", "persons", 500, 200, 4000, 0);
        auxVar.AddAction(2, 0.5f);
        VariablesHelper.baseVariables.Add(auxVar);
        auxVar = new GameVariable("Food", "kg", 5000, 0, 6000, 2000);
        VariablesHelper.baseVariables.Add(auxVar);
        auxVar = new GameVariable("Happiness", "%", 60, 0, 100, 0);
        VariablesHelper.baseVariables.Add(auxVar);

        auxGroup = new GamePercentGroup();
        auxVar = new GameVariable("City Land", "m2", 10, 400, 100, 0);
        auxGroup.AddToGroup(auxVar);
        auxVar = new GameVariable("Farming Land", "m2", 30, 600, 100, 0);
        auxGroup.AddToGroup(auxVar);
        auxVar = new GameVariable("Forest", "m2", 40, -2000, 100, 0);
        auxGroup.AddToGroup(auxVar);
        auxVar = new GameVariable("Sea", "m2", 20, 0, 100, 0);
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

        auxAction = new Action("Organize big party on your city");
        auxAction.AddVariableAction(0, 400);
        auxAction.AddVariableAction(3, 20);
        VariablesHelper.actions.Add(auxAction);
    }
}
