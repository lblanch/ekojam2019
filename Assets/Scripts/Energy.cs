using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy
{
    float clean;
    float dirty;

    public Energy(float _clean)
    {
        if (_clean <= 100)
        {
            clean = _clean;
            dirty = 100 - _clean;
        }
        else //wrong amounts provided, we initialize each value equally
        {
            clean = 100/2;
            dirty = _clean;
        }
    }
    
    void IncreaseCleanEnergy(float amount = 10)
    {
        if (amount <= dirty)
        {
            clean += amount;
            dirty -= amount;
        }
    }

    void IncreaseDirtyEnergy(float amount = 10)
    {
        if(amount <= clean)
        {
            clean -= amount;
            dirty += amount;
        }

    }

}