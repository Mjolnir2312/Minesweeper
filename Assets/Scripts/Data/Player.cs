using DP.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Player
{
    static Player()
    {
        Player.data = DPPlayerPrefs.GetObjectValue<Player.Data>("PlayerDataSettings");
        if (data == null)
        {
            data = new Data
            {
                Coin = 50,
                Sound = 1,
                Vibration = true,
                AutoOpen = false,
                HoldToFlag = true,
                Zoom = true,
                DragBoard = true,
                ShowFrame = true,
                EasyTime = 0,
                MediumTime = 0,
                HardTime = 0,
                ExtremeTime = 0,
                GamePlays = 0,
                GamePlayWon = 0,
                GamePlayRatio = 0,
                EasyPlays = 0,
                EasyWon = 0,
                EasyRatio = 0,
                MediumPlays = 0,
                MediumWon = 0,
                MediumRatio = 0,
                HardPlays = 0,
                HardWon = 0,
                HardRatio = 0,
                ExtremePlays = 0,
                ExtremeWon = 0,
                ExtremeRatio = 0
            };
            SaveData();
        }
    }



    public static void AddOrRemoveCoin(int _count)
    {
        data.Coin += _count;

        if (data.Coin < 0)
        {
            data.Coin = 0;
        }
        SaveData();
    }

    public static float ShowCoin()
    {
        return data.Coin;
    }

    public static int GetSound(string _sound, int defaultValue)
    {
        return PlayerPrefs.GetInt(_sound, defaultValue);
    }

    public static bool SetSound(string _sound, bool sound)
    {
        data.Sound = (!sound) ? 0 : 1;
        SaveData();
        return GetSound(_sound, data.Sound) == 1;
    }

    public static void SetVibration(bool vibration)
    {
        data.Vibration = !data.Vibration;
        SaveData();
    }

    public static void SetAutoOpen()
    {
        data.AutoOpen = !data.AutoOpen;
        SaveData();
    }

    public static void SetHoldToFlag()
    {
        data.HoldToFlag = !data.HoldToFlag;
        SaveData();
    }

    public static void SetZoom()
    {
        data.Zoom = !data.Zoom;
        SaveData();
    }

    public static void SetDrag()
    {
        data.DragBoard = !data.DragBoard;
        SaveData();
    }

    public static void SetShowFrame()
    {
        data.ShowFrame = !data.ShowFrame;
        SaveData();
    }



    #region STATS

    public static void BestTime(float _time, GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.Easy:
                if (data.EasyTime == 0)
                {
                    data.EasyTime = _time;
                    SaveData();
                }
                else
                {
                    CompareTime(_time, gameMode);
                }

                break;

            case GameMode.Medium:
                if (data.MediumTime == 0)
                {
                    data.MediumTime = _time;
                    SaveData();
                }
                else
                {
                    CompareTime(_time, gameMode);
                }
                break;

            case GameMode.Hard:
                if (data.HardTime == 0)
                {
                    data.HardTime = _time;
                    SaveData();
                }
                else
                {
                    CompareTime(_time, gameMode);
                }
                break;

            case GameMode.Extreme:
                if (data.ExtremeTime == 0)
                {
                    data.ExtremeTime = _time;
                    SaveData();
                }
                else
                {
                    CompareTime(_time, gameMode);
                }
                break;

            default:
                break;
        }

    }

    public static float ShowTime(float _time, GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.Easy:
                return data.EasyTime;

            case GameMode.Medium:
                return data.MediumTime;

            case GameMode.Hard:
                return data.HardTime;

            case GameMode.Extreme:
                return data.ExtremeTime;
        }
        return ShowTime(_time, gameMode);
    }

    public static void CompareTime(float _time, GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.Easy:
                if (_time < data.EasyTime)
                {
                    data.EasyTime = _time;
                    SaveData();
                }
                break;

            case GameMode.Medium:
                if (_time < data.MediumTime)
                {
                    data.MediumTime = _time;
                    SaveData();
                }
                break;

            case GameMode.Hard:
                if (_time < data.HardTime)
                {
                    data.HardTime = _time;
                    SaveData();
                }
                break;

            case GameMode.Extreme:
                if (_time < data.ExtremeTime)
                {
                    data.ExtremeTime = _time;
                    SaveData();
                }
                break;

            default:
                break;
        }

    }

    public static float EasyTimeRecord()
    {
        return data.EasyTime;
    }

    #endregion
    public static void SaveData()
    {
        DPPlayerPrefs.SetObjectValue<Player.Data>("PlayerDataSettings", Player.data, false);
    }

    private static Player.Data data;
    [SerializeField]
    public class Data
    {
        public int Coin;
        public int Sound;
        public bool Vibration;
        public bool AutoOpen;
        public bool HoldToFlag;
        public bool Zoom;
        public bool DragBoard;
        public bool ShowFrame;

        public float EasyTime;
        public float MediumTime;
        public float HardTime;
        public float ExtremeTime;

        public int GamePlays;
        public int GamePlayWon;
        public float GamePlayRatio;

        public int EasyPlays;
        public int EasyWon;
        public float EasyRatio;

        public int MediumPlays;
        public int MediumWon;
        public float MediumRatio;

        public int HardPlays;
        public int HardWon;
        public float HardRatio;

        public int ExtremePlays;
        public int ExtremeWon;
        public float ExtremeRatio;
    }

}

