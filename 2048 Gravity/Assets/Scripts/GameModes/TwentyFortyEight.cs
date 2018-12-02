using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwentyFortyEight : GameMode
{
    public float chanceOf4 = 0.15f;

    int score = 0;
    Material[] materials;

    public TwentyFortyEight()
    {
        materials = LoadMaterials();
    }

    public int GetScore()
    {
        return score;
    }

    public void BallMerged(GameObject ball, int value)
    {
        if(ball != null)
        {
            Debug.Assert(value > 0);
            score += value;
        }
    }

    public void BallDestroyed(GameObject ball, int value)
    {
        if(ball != null)
        {
            Debug.Assert(value > 0);
            score -= value;
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public string DisplayStatus()
    {
        return string.Format("{0} points", score);
    }

    public Material GetMaterial(int val)
    {
        return materials[ValueToIndex(val)];
    }

    public bool ShouldMerge(int a, int b)
    {
        return a == b;
    }

    public int Merge(int a, int b)
    {
        if(a != b)
        {
            throw new InvalidOperationException("Incompatible merge.");
        }
        return a * 2;
    }

    public bool IsTargetValue(int val)
    {
        return val == 2048;
    }

    public int GetInitialValue()
    {
        chanceOf4 = Mathf.Clamp01(chanceOf4);
        float rand01 = UnityEngine.Random.value;
        if (rand01 < chanceOf4)
        {
            return 4;
        }
        return 2;
    }

    public bool ShouldDisplayValue()
    {
        return true;
    }

    Material[] LoadMaterials()
    {
        Material[] mats = new Material[12];
        mats[0]  = Resources.Load<Material>("TwentyFortyEight/2");
        mats[1]  = Resources.Load<Material>("TwentyFortyEight/4");
        mats[2]  = Resources.Load<Material>("TwentyFortyEight/8");
        mats[3]  = Resources.Load<Material>("TwentyFortyEight/16");
        mats[4]  = Resources.Load<Material>("TwentyFortyEight/32");
        mats[5]  = Resources.Load<Material>("TwentyFortyEight/64");
        mats[6]  = Resources.Load<Material>("TwentyFortyEight/128");
        mats[7]  = Resources.Load<Material>("TwentyFortyEight/256");
        mats[8]  = Resources.Load<Material>("TwentyFortyEight/512");
        mats[9]  = Resources.Load<Material>("TwentyFortyEight/1024");
        mats[10] = Resources.Load<Material>("TwentyFortyEight/2048");
        mats[11] = Resources.Load<Material>("TwentyFortyEight/4096+");
        int nulls = 0;
        for(int i = 0; i < mats.Length; i++)
        {
            if (mats[i] == null)
                nulls++;
        }
        Debug.LogWarningFormat("{0} materials failed to load.", nulls);
        return mats;
    }

    /// <summary>
    /// Takes a value that is a power of two, and converts it into the array index we need in the materials array.
    /// </summary>
    /// <param name="val">A power of two.</param>
    /// <returns>The index in the materials array.</returns>
    /// <exception cref="ArgumentException">If a negative, 0, or non-power-of-two number was given.</exception>
    int ValueToIndex(int val)
    {
        if (val <= 0)
        {
            throw new ArgumentException("Must call ValueToIndex on a positive number.");
        }
        if (!IsPowerOfTwo(val))
        {
            throw new ArgumentException("Must call ValueToIndex on a power of two.");
        }

        float logBase2 = Mathf.Log(val, 2);
        //make sure it is an integer
        Debug.Assert(logBase2 == Mathf.Floor(logBase2));

        int index = (int)logBase2 - 1;
        if (index >= materials.Length)
        {
            index = materials.Length - 1;
        }
        return index;
    }

    bool IsPowerOfTwo(int num)
    {
        if (num < 0)
        {
            throw new NotImplementedException("This function only works with positive numbers.");
        }

        const int bitsInInt = 32;
        //Just count bits. If there is a single 1 and all the rest are 0, then we have a power of 2.
        int onesFound = 0;
        //written this way so the compiler can easily unroll it. This method should be branchless in an optimized build.
        for (int i = 0; i < bitsInInt; i++)
        {
            onesFound += num & 1;
            num >>= 1;
        }
        return onesFound == 1;
    }
}
