using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuddenDeathMode //: GameMode
{
    int livesRemaining = 1;

    public SuddenDeathMode() { }

    public int GetScore()
    {
        return livesRemaining;
    }

    public void BallMerged(GameObject ball, int value)
    {

    }

    public void BallDestroyed(GameObject ball, int value)
    {
        if(ball != null)
        {
            --livesRemaining;
            EndGame();
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public string DisplayStatus()
    {
        return "Don't drop any balls!";
    }

    public Material GetMaterial(int currentValue)
    {
        return null;
    }

    public bool ShouldMerge(int a, int b)
    {
        return false;
    }

    public bool IsTargetValue(int val)
    {
        return false;
    }

    public int GetInitialValue()
    {
        return 1;
    }
}
