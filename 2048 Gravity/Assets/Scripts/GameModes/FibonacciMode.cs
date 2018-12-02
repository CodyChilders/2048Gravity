using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FibonacciMode : GameMode
{
    Material[] materials;
    int[] knownFibonacciNumbers;
    int score;

    public FibonacciMode()
    {
        materials = LoadMaterials();
        InitializeFibonacciLookup();
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
        if (ball != null)
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
        Debug.Assert(IsFibonacci(val));
        return materials[FibonacciIndex(val)];
    }

    public bool ShouldMerge(int a, int b)
    {
        return SequentialFibonacci(a, b);
    }

    public int Merge(int a, int b)
    {
        if(!IsFibonacci(a) || !IsFibonacci(b))
        {
            throw new InvalidOperationException("Not fibonacci numbres.");
        }
        if(!SequentialFibonacci(a, b))
        {
            throw new InvalidOperationException("Cannot merge when the values are non-consecutive fibonacci.");
        }
        return a + b;
    }

    public bool IsTargetValue(int val)
    {
        return val == 55;
    }

    public int GetInitialValue()
    {
        return 1;
    }

    public bool ShouldDisplayValue()
    {
        return true;
    }

    Material[] LoadMaterials()
    {
        Material[] mats = new Material[10];
        mats[0] = Resources.Load<Material>("Fibonacci/1");
        mats[1] = Resources.Load<Material>("Fibonacci/2");
        mats[2] = Resources.Load<Material>("Fibonacci/3");
        mats[3] = Resources.Load<Material>("Fibonacci/5");
        mats[4] = Resources.Load<Material>("Fibonacci/8");
        mats[5] = Resources.Load<Material>("Fibonacci/13");
        mats[6] = Resources.Load<Material>("Fibonacci/21");
        mats[7] = Resources.Load<Material>("Fibonacci/34");
        mats[8] = Resources.Load<Material>("Fibonacci/55");
        mats[9] = Resources.Load<Material>("Fibonacci/89+");
        return mats;
    }

    void InitializeFibonacciLookup()
    {
        //we're starting the sequence at [1, 2] instead of
        //[0, 1] because 0 is never displayed, and the value
        //serves double purpose as the material array index.
        List<int> knownFibs = new List<int>();
        knownFibs.Add(1);
        knownFibs.Add(2);

        //we'll load this up with a few too many just to be sure.
        while(knownFibs.Count < 20)
        {
            int last = knownFibs[knownFibs.Count - 1];
            int nextToLast = knownFibs[knownFibs.Count - 2];
            knownFibs.Add(last + nextToLast);
        }

        knownFibonacciNumbers = knownFibs.ToArray();
    }

    bool IsFibonacci(int val)
    {
        return FibonacciIndex(val) >= 0;
    }

    int FibonacciIndex(int val)
    {
        int arrayIndex = Array.BinarySearch(knownFibonacciNumbers, val);
        if(arrayIndex < 0)
        {
            throw new InvalidOperationException("The input is not a fibonacci number.");
        }
        return arrayIndex;
    }

    bool SequentialFibonacci(int a, int b)
    {
        Debug.Assert(IsFibonacci(a) && IsFibonacci(b));

        //special case - merging two 1's
        if (a == 1 && b == 1)
            return true;

        int aIndex = FibonacciIndex(a);
        int bIndex = FibonacciIndex(b);
        return Mathf.Abs(aIndex - bIndex) == 1;
    }
}
