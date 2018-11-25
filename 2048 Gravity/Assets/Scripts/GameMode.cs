using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface GameMode
{
    int GetScore();
    void BallMerged(GameObject ball, int value);
    void BallDestroyed(GameObject ball, int value);
    void EndGame();
    string DisplayStatus();
} 
