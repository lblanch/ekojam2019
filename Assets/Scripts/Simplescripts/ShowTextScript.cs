using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextScript : MonoBehaviour
{
    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.text = VariablesHelper.gameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
