using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GameState
{
    static GameMode currentMode = null;

    public static GameMode GetGameController()
    {
        //default mode for when debugging and not starting from the main menu.
        if(currentMode == null)
        {
            Debug.LogWarning("No game mode was selected when a Game Controller was accessed, so it defaulted to free play.");
            ConfigureForFreePlay();
        }
        return currentMode;
    }

    public static void ConfigureForSuddenDeath()
    {
        currentMode = new SuddenDeathMode();
    }

    public static void ConfigureForLives()
    {
        currentMode = new LivesMode();
    }

    public static void ConfigureForFreePlay()
    {
        currentMode = new FreePlayMode();
    }
}
