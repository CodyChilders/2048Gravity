using System;
using UnityEngine;

interface GameMode
{
    int GetScore();
    void BallMerged(GameObject ball, int value);
    void BallDestroyed(GameObject ball, int value);
    void EndGame();
    string DisplayStatus();
    Material GetMaterial(int currentValue);
    bool ShouldMerge(int val1, int val2);
    int Merge(int val1, int val2);
    bool IsTargetValue(int value);
    int GetInitialValue();
    bool ShouldDisplayValue();
} 
