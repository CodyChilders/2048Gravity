using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RainbowMode : GameMode
{
    int highest = 0;
    Material[] materials;

    public RainbowMode()
    {
        materials = LoadMaterials();
    }

    public int GetScore()
    {
        return -1;
    }

    public void BallMerged(GameObject ball, int value)
    {
        if(ball != null)
        {
            if (value > highest)
                highest = value;
        }
    }

    public void BallDestroyed(GameObject ball, int value)
    {
        //nothing
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public string DisplayStatus()
    {
        string color;
        switch(highest)
        {
            case 0:
                color = "Red";
                break;
            case 1:
                color = "Orange";
                break;
            case 2:
                color = "Yellow";
                break;
            case 3:
                color = "Green";
                break;
            case 4:
                color = "Blue";
                break;
            case 5:
                color = "Indigo";
                break;
            case 6:
                color = "Violet";
                break;
            default:
                color = "Ultraviolet";
                break;
        }
        return string.Format("Highest color: {0}", color);
    }

    public Material GetMaterial(int val)
    {
        if (val >= materials.Length)
            return materials[materials.Length - 1];
        return materials[val];
    }

    public bool ShouldMerge(int a, int b)
    {
        return a == b;
    }

    public int Merge(int a, int b)
    {
        if (a != b)
        {
            throw new InvalidOperationException("Incompatible merge.");
        }
        return a + 1;
    }

    public bool IsTargetValue(int val)
    {
        return val == materials.Length - 1;
    }

    public int GetInitialValue()
    {
        return 0;
    }

    public bool ShouldDisplayValue()
    {
        return false;
    }

    Material[] LoadMaterials()
    {
        Material[] mats = new Material[7];
        mats[0] = Resources.Load<Material>("Rainbow/Red");
        mats[1] = Resources.Load<Material>("Rainbow/Orange");
        mats[2] = Resources.Load<Material>("Rainbow/Yellow");
        mats[3] = Resources.Load<Material>("Rainbow/Green");
        mats[4] = Resources.Load<Material>("Rainbow/Blue");
        mats[5] = Resources.Load<Material>("Rainbow/Indigo");
        mats[6] = Resources.Load<Material>("Rainbow/Violet");
        return mats;
    }
}
