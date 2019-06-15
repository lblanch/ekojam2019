using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    private bool toggled = false;
    //Tells if the option is chosen or not. Should get returned in a method later with the data accompanied by the button.
    public Sprite toggledSprite;
    public Sprite untoggledSprite;
    //Sprites for different states of the button.

    public void ButtonToggled ()
    //Toggles the button on if it's off an visa versa. Also handles changing the sprites.
    {
        int actionId = int.Parse(transform.name.Substring(7));

        if (toggled)
        {
            VariablesHelper.actions[actionId].CalculateActionChanges(-1);
            toggled = false;
            GetComponent<Image>().sprite = untoggledSprite;
        }
        else
        {
            VariablesHelper.actions[actionId].CalculateActionChanges();
            toggled = true;
            GetComponent<Image>().sprite = toggledSprite;
        }
    }

    public bool IsToggled()
    {
        return toggled;
    }
}
