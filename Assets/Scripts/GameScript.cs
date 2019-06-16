using System;
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
    private int maxActions;
    private int CO2Index;
    private int cyclesCompleted;

    // Start is called before the first frame update
    void Start()
    {
        maxActions = ACTIONS_PER_CYCLE;
        cyclesCompleted = 0;

        LoadData();

        if (maxActions > VariablesHelper.actions.Count)
        {
            maxActions = VariablesHelper.actions.Count;
        }

        GenerateRandomActionButtons();
    }


    void FixedUpdate()
    {
        bool auxVarValues = true;

        textAllVariables.text = "CURRENT STATS\n\n";
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            textAllVariables.text += "- " + var.name + " " + var.minValue + "..." + var.value + "..." + var.maxValue + " " + var.unit + "\n";
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
                textAllVariables.text += "- " + varVar.name + " " + varVar.minValue + "..." + varVar.value + "..." + varVar.maxValue + " " + varVar.unit + "\n";
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
        string cycleActions = "Cycle actions: ";
        string consequences = "Consequences: ";
        Tuple<bool, string> auxTuple;

        /*if(!varValuesAreOk)
        {
            //TODO show this message to the user in a pop up window or something
            Debug.Log(" With the current combination of actions, some of the stats have a wrong value. Please choose differently!");
            return;
        }*/
       
        //execute all actions and delete buttons
        GameObject.Find("Choices").GetComponentInChildren<Button>(false);
        foreach (Button actionButton in GameObject.Find("Choices").GetComponentsInChildren<Button>(false))
        {
            if (actionButton.GetComponentInChildren<ToggleButton>().IsToggled())
            {
                Debug.Log("Executing " + actionButton.name);
                actionId = int.Parse(actionButton.transform.name.Substring(7));
                VariablesHelper.actions[actionId].ExecuteAction();
            }
            Destroy(actionButton.gameObject);
            Debug.Log("Destroyed " + actionButton.name + " button");
        }

        //execute all actions of our variables
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            cycleActions += var.ExecuteVarActions();
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
            {
                cycleActions += varVar.ExecuteVarActions();
            }
        }
        Debug.Log(cycleActions);

        //TODO generate CO2
        //TODO CO2 generation could be a cycleAction
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            cycleActions += var.name + " generated: " + var.ExecuteVarActions();
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
            {
                cycleActions += varVar.name + " generated: " + varVar.ExecuteVarActions();
            }
        }

        //TODO update max and min values

        //check end game conditions and consequences
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            auxTuple = CheckConsequences(var);
            if(auxTuple.Item1)
            {
                //GAME OVER
                //TODO to to game over scene
                Debug.Log(auxTuple.Item2);
                return;
            }
            else
            {
                consequences += auxTuple.Item2;
            }
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
            {
                auxTuple = CheckConsequences(varVar);
                if (auxTuple.Item1)
                {
                    //GAME OVER
                    //TODO to to game over scene
                    Debug.Log(auxTuple.Item2);
                    return;
                }
                else
                {
                    consequences += auxTuple.Item2;
                }
            }
        }
        Debug.Log(consequences);

        cyclesCompleted++;

        if(cyclesCompleted == CYCLE_AMOUNT)
        {
            //TODO go to win scene
            Debug.Log("GAME OVER --> WIN");
            return;
        }
        GenerateRandomActionButtons();
    }

    Tuple<bool,string> CheckConsequences(GameVariable var)
    {
        string returnStr = "";
        //check max value
        if (var.value > var.maxValue)
        {
            if (var.maxConsequence.isGameOver)
            {
                //GAME OVER
                returnStr = "GAME OVER -> " + var.name + " value: " + var.value + " max value: " + var.maxValue;
                return new Tuple<bool, string> (true, returnStr);
            }
            else
            {
                returnStr += var.maxConsequence.ExecuteConsequenceActions();
            }
        }
        //check min value
        if (var.value < var.minValue)
        {
            if (var.minConsequence.isGameOver)
            {
                //GAME OVER
                returnStr = "GAME OVER -> " + var.name + " value: " + var.value + " min value: " + var.minValue;
                return new Tuple<bool, string>(true, returnStr);
            }
            else
            {
                returnStr += var.minConsequence.ExecuteConsequenceActions();
            }
        }

        return new Tuple<bool, string>(false, returnStr);
    }

    void GenerateRandomActionButtons()
    {
        int auxIndex = 0;
        //create one button per each action, randomly selected
        for (int i = 0; i < maxActions; i++)
        {
            auxIndex = UnityEngine.Random.Range(0, maxActions - 1);

            GameObject newButton = GameObject.Instantiate(ActionButtonPrefab);
            newButton.transform.SetParent(GameObject.Find("Choices").transform, false);
            newButton.name = "action_" + auxIndex;
            //TODO Store the actionId somewhere in the button object, if possible
            //TODO Adjust button height to fit all text
            newButton.GetComponentInChildren<Text>().text = VariablesHelper.actions[auxIndex].description + "\n" + VariablesHelper.actions[auxIndex].ActionsToString();

            //TODO find a way to programatically get the height of the button
            //newButton.GetComponent<RectTransform>().rect.size.y doesn't seem to take the objects scaling
            newButton.transform.position = newButton.transform.position + new Vector3(0, 15, 0) + new Vector3(0, i * -65, 0);
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
        auxVar.maxConsequence.isGameOver = true;
        auxVar.minConsequence.isGameOver = false;
        VariablesHelper.baseVariables.Add(auxVar);
        CO2Index = 0;

        auxVar = new GameVariable("Population", "persons", 500, 200, 4000, 0);
        //eat food
        auxVar.AddAction(2, -0.5f);
        auxVar.maxConsequence.isGameOver = false;
        auxAction = new Action("People too cramped, unhappy!");
        auxAction.AddVariableAction(3, -20);
        auxVar.maxConsequence.AddConsequenceActions(auxAction);
        auxVar.minConsequence.isGameOver = true;
        VariablesHelper.baseVariables.Add(auxVar);

        auxVar = new GameVariable("Food", "kg", 5000, 0, 6000, 2000);
        auxVar.maxConsequence.isGameOver = false;
        auxAction = new Action("People unhappy we are trowing away food!");
        auxAction.AddVariableAction(3, -20);
        auxVar.maxConsequence.AddConsequenceActions(auxAction);
        auxVar.minConsequence.isGameOver = false;
        auxAction = new Action("People migrate out of country because of starvation!");
        auxAction.AddVariableAction(1, -20);
        auxVar.minConsequence.AddConsequenceActions(auxAction);
        auxAction = new Action("People unhappy because of starvation!");
        auxAction.AddVariableAction(3, -20);
        auxVar.minConsequence.AddConsequenceActions(auxAction);
        VariablesHelper.baseVariables.Add(auxVar);

        auxVar = new GameVariable("Happiness", "%", 60, 0, 100, 0);
        auxVar.minConsequence.isGameOver = true;
        auxVar.maxConsequence.isGameOver = false;
        VariablesHelper.baseVariables.Add(auxVar);

        auxGroup = new GamePercentGroup();
        auxVar = new GameVariable("City Land", "m2", 10, 400, 100, 0);
        auxVar.minConsequence.isGameOver = true;
        auxVar.maxConsequence.isGameOver = true;
        auxGroup.AddToGroup(auxVar);
        auxVar = new GameVariable("Farming Land", "m2", 30, 600, 100, 0);
        auxVar.minConsequence.isGameOver = true;
        auxVar.maxConsequence.isGameOver = true;
        auxVar.AddAction(2, 1.0f);
        auxGroup.AddToGroup(auxVar);
        auxVar = new GameVariable("Forest", "m2", 40, -2000, 100, 0);
        auxVar.minConsequence.isGameOver = true;
        auxVar.maxConsequence.isGameOver = true;
        auxGroup.AddToGroup(auxVar);
        auxVar = new GameVariable("Sea", "m2", 20, 0, 100, 0);
        auxVar.minConsequence.isGameOver = true;
        auxVar.maxConsequence.isGameOver = true;
        auxGroup.AddToGroup(auxVar);
        VariablesHelper.groupVariables.Add(auxGroup);

        //add actions to action vector
        auxAction = new Action("Increase city land");
        auxAction.AddGroupAction(0, 2, 0, 20);
        auxAction.AddVariableAction(1, 10);
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

        //add CO2 cycle Action for all variables
        foreach (GameVariable var in VariablesHelper.baseVariables)
        {
            var.AddAction(CO2Index, var.CalculateCO2());
        }
        foreach (GamePercentGroup var in VariablesHelper.groupVariables)
        {
            foreach (GameVariable varVar in var.variables)
            {
                varVar.AddAction(CO2Index, varVar.CalculateCO2());
            }
        }
    }
}
