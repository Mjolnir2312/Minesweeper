using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSettings
{
    #region Settings Default
    public static GameSettings Easy = new GameSettings(9, 9, 10);
    [SerializeField] private int _height;// row = hang
    [SerializeField] private int _width;// column = cot
    [SerializeField] private int _mines;
    #endregion
    public int Height
    {
        get { return _height; }
    }

    public int Width
    {
        get { return _width; }
    }

    public int Mines
    {
        get { return _mines; }
        set { _mines = value; }
    }

    public GameSettings(int h, int w, int m)
    {
        _width = w;
        _height = h;
        _mines = m;
    }

    public GameSettings()
    {

    }

    public void Set(int h, int w, int m)
    {
        _width = w;
        _height = h;
        _mines = m;
    }
}
