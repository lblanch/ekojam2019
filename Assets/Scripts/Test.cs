using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(ForestCalculations.GetForestCO2EmissionsKtCO2(100000) / 1000 + " Mt of CO2 forest emissions");
    }
}
