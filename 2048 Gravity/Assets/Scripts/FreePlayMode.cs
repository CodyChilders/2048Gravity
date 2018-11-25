using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreePlayMode : GameMode
{
    int score = 0;

    public FreePlayMode() { }

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
}
