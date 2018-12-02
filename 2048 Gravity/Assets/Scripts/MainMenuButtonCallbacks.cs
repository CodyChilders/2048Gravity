using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonCallbacks : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void SuddenDeathButton()
    {
        GameState.ConfigureForSuddenDeath();
        SceneManager.LoadScene("Game");
    }

    public void TenLivesButton()
    {
        GameState.ConfigureForLives();
        SceneManager.LoadScene("Game");
    }

    public void TwentyFortyEightButton()
    {
        GameState.ConfigureFor2048();
        SceneManager.LoadScene("Game");
    }

    public void RainbowButton()
    {
        GameState.ConfigureForRainbow();
        SceneManager.LoadScene("Game");
    }

    public void FibonacciButton()
    {
        GameState.ConfigureForFibonacci();
        SceneManager.LoadScene("Game");
    }
}
