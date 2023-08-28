using DP.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    static Player()
    {
        Player.data = DPPlayerPrefs.GetObjectValue<Player.Data>("PlayerData");
        if(data == null)
        {
            data = new Data
            {
                Coin = 50,
                Sound = true,
                Vibration = true,
                AutoOpen = false,
                HoldToFlag = true,
                Zoom = true,
                DragBoard = true,
                ShowFrame = true,
            };

            SaveData();
        }
    }

    public static void AddOrRemoveCoin(int _count)
    {
        data.Coin += _count;

        if(data.Coin < 0) 
        { 
            data.Coin = 0;
        }
        SaveData(); 
    }

    public static float ShowCoin()
    {
        return data.Coin;
    }

    public static void SetSound(bool sound)
    {
        data.Sound = sound;
        Debug.Log(sound);
        SaveData();
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

    public static void SaveData()
    {
        DPPlayerPrefs.SetObjectValue<Player.Data>("PlayerData", Player.data, false);
    }

    private static Player.Data data;
    [SerializeField]
    public class Data
    {
        public int Coin;
        public bool Sound;
        public bool Vibration;
        public bool AutoOpen;
        public bool HoldToFlag;
        public bool Zoom;
        public bool DragBoard;
        public bool ShowFrame;


    }
}

