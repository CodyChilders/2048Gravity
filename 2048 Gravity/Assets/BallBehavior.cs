﻿using System;
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
    Camera camera = null;

    void Start()
    {
        SetMaterial();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Debug.Assert(camera != null);
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnGUI()
    {
        const int labelWidth = 80;
        const int labelHeight = 20;
        Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        /*
        Rect rect = new Rect(screenPosition.x - (labelWidth / 2), 
                             screenPosition.y - (labelHeight / 2), 
                             labelWidth, labelHeight);
        */
        Rect rect = new Rect(screenPosition.x, screenPosition.y, labelWidth, labelHeight);
        GUI.contentColor = Color.black;
        GUI.Label(rect, currentValue.ToString());
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
    /// <exception cref="ArgumentException">If a negative, 0, or non-power-of-two number was given.</exception>
    int ValueToIndex(int val)
    {
        if(val <= 0)
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
