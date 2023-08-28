using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ThemeData : ScriptableObject
{
    public List<ThemeDataCost> themeDataCost;
}

[Serializable]
public class ThemeDataCost
{
    public Themes themeId;
    public int Cost;
    public Sprite TileUnknow, TilePressed;
    public Sprite Tile0, Tile1, Tile2, Tile3, Tile4, Tile5, Tile6, Tile7, Tile8;
    public Sprite TileMine, TileFlag, TileQuestion, TileFlagMode;
    public Sprite TileDead, TileWrong;
}

public enum Themes
{
    Theme1,
    Theme2,
    Theme3,
    Theme4,
    Theme5,
    Theme6,
    Theme7,
    Theme8,
    Theme9
}

