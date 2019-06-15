using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    private bool toggled = false;
    public Sprite toggledSprite;
    public Sprite untoggledSprite;

    public void ButtonToggled ()
    {
        if (toggled)
        {
            toggled = false;
            GetComponent<Image>().sprite = untoggledSprite;
        }
        else
        {
            toggled = true;
            GetComponent<Image>().sprite = toggledSprite;
        }
    }
}
