using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land
{
    public float farming;
    public float city;
    public float forest;

    public Land(float _farming, float _city) {
        if((_farming + _city) < 100)
        {
            farming = _farming;
            city = _city;
            forest = 100 - _farming - _city;
        }
        else //wrong amounts provided, we initialize each value equally
        {
            farming = 100 / 3;
            forest = farming;
            city = farming;
        }
    }

    //city land is always taken from forest land
    public void IncreaseCityLand(float amount = 10)
    {
        if(amount <= forest)
        { 
            forest -= amount;
            city += amount;
        }

    }

    //farming land is always taken from forest land
    public void IncreaseFarming(float amount = 10)
    {
        if (amount <= forest)
        {
            forest -= amount;
            farming += amount;
        }
    }

    //forest is always planted on farm land
    public void IncreaseForest(float amount = 10)
    {
        if (amount <= farming)
        {
            forest += amount;
            farming -= amount;
        }
    }

}