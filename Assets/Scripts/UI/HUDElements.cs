using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]

public class HUDElements
{
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _flagText;
    [SerializeField] private StateButton _gameState;


    public Text TimerText
    {
        get { return _timerText; }
        set { _timerText = value;}
    }

    public Text FlagText
    {
        get { return _flagText; }
        set { _flagText = value;}
    }

    public StateButton GameState
    {
        get { return _gameState; }
        set { _gameState = value;}
    }
}

