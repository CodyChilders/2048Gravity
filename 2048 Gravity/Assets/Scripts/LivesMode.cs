using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class LivesMode : GameMode
{
    int livesRemaining = 10;

    public LivesMode() { }

    public int GetScore()
    {
        return livesRemaining;
    }

    public void BallMerged(GameObject ball, int value)
    {

    }

    public void BallDestroyed(GameObject ball, int value)
    {
        if (ball != null)
        {
            if (--livesRemaining == 0)
            {
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public string DisplayStatus()
    {
        return string.Format("{0} lives remaining.", livesRemaining);
    }
}
