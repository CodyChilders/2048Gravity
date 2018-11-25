using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    Text txtDisplay;

    void Start()
    {
        txtDisplay = GetComponentInChildren<Text>();
    }


    void Update()
    {
        txtDisplay.text = GameState.GetGameController().DisplayStatus();
    }
}
