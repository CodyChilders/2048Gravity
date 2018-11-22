using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Material[] materials = new Material[12];
    public int CurrentValue
    {
        get
        {
            return currentValue;
        }
    }

    int currentValue = 2;

    void Start()
    {
        SetMaterial();
    }

    void Update()
    {

    }

    void SetMaterial()
    {
        Material desiredMaterial = materials[ValueToIndex(currentValue)];
        GetComponent<Renderer>().material = desiredMaterial;
    }

    /// <summary>
    /// Takes a value that is a power of two, and converts it into the array index we need in the materials array.
    /// </summary>
    /// <param name="val">A power of two.</param>
    /// <returns>The index in the materials array.</returns>
    int ValueToIndex(int val)
    {
        if(val < 0)
        {
            throw new ArgumentException("Must call ValueToIndex on a positive number.");
        }
        if(!IsPowerOfTwo(val))
        {
            throw new ArgumentException("Must call ValueToIndex on a power of two.");
        }

        float logBase2 = Mathf.Log(val, 2);
        //make sure it is an integer
        Debug.Assert(logBase2 == Mathf.Floor(logBase2));

        int index = (int)logBase2 - 1;
        if(index >= materials.Length)
        {
            index = materials.Length - 1;
        }
        return index;
    }

    bool IsPowerOfTwo(int num)
    {
        const int bitsInInt = 32;
        //Just count bits. If there is a single 1 and all the rest are 0, then we have a power of 2.
        int onesFound = 0;
        //written this way so the compiler can easily unroll it. This method should be branchless in an optimized build.
        for(int i = 0; i < bitsInInt; i++)
        {
            onesFound += num & 1;
            num >>= 1;
        }
        return onesFound == 1;
    }
}
