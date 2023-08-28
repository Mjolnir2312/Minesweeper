using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataSetting : ScriptableObject
{
    public List<GameDataMode> dataMode;
}


[Serializable]
public class GameDataMode
{
    public GameMode mode;
    public int Height;
    public int Width;
    public int Mine;
    public int coinReward;
    public float UpGrid;
    public float fieldOfViewCam;
}

public enum GameMode
{
    Easy,
    Medium,
    Hard,
    Extreme,
    Custom
}

