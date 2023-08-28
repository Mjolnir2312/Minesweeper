using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TileScript;

public class GameManager : MonoBehaviour
{
    #region Properties
    public static GameManager Instance { get; private set; }
    public static bool IsGameOver;
    public ModeGamePanel ModeGamePanel;
    public DataSetting gameData;
    public ThemeData themeData;
    public Sprite[] spriteGameStateArray;
    public Image gameStateImg;

    [SerializeField] GridControl _grid;
    [SerializeField] private Camera mainCamera;
    public GameMode selectedMode;

    [Header("Win")]
    [SerializeField] private Text record;
    [SerializeField] private Text bombFound;
    [SerializeField] private Text winReward;
    [SerializeField] private Text coinReward;

    [Header("Lose")]
    [SerializeField] private Text loseBombFound;
    [SerializeField] private Text lostCoinReward;
    #endregion

    #region Get/Set

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        //selectedMode = GameMode.Medium;
        StartGame(selectedMode);
    }
    private void Awake()
    {
        _grid = GameObject.Find("GridManager").GetComponent<GridControl>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //StartGame(GameMode.Easy);
    }

    void Update()
    {
        //Debug.Log(selectedMode);
    }

    //public GameMode GetSelectMode()
    //{
    //    return selectedMode;
    //}
    #endregion

    #region MAIN

  
    public void StartGame(GameMode mode)
    {
        GameDataMode gameDataMode = GetGameDataModeFromGameMode(mode);
        if (gameDataMode != null)
        {
            if (_grid != null)
            {
                _grid.DestroyTiles();
            }

            _grid.StartNewGame(gameDataMode);
            mainCamera.fieldOfView = gameDataMode.fieldOfViewCam; 
            IsGameOver = false;
            gameStateImg.sprite = spriteGameStateArray[0];
        }
    }


    public GameDataMode GetGameDataModeFromGameMode(GameMode mode)
    {
        foreach (var dataMode in gameData.dataMode)
        {
            if (dataMode.mode == mode)
            {
                return dataMode;
            }
        }
        return null;
    }

    public void GameOver(bool win)
    {
        IsGameOver = true;
        
        if (win)
        {
            ModeGamePanel.OnWinForm(true);
            SoundManager.Instance.WinSound();
            gameStateImg.sprite = spriteGameStateArray[2];
            //WinGameReward(selectedMode);
        }
        else
        {
            ModeGamePanel.OnContinueForm(true);
            SoundManager.Instance.LoseSound();
            gameStateImg.sprite = spriteGameStateArray[3];
            //LostGameReward();
        }
    }

    public void Reset()
    {
        _grid.Restart();
        gameStateImg.sprite = spriteGameStateArray[0];
        IsGameOver = false;
    }

    public void ContinueWhenLoseManager()
    {
        _grid.ContinueWhenLose();
        gameStateImg.sprite = spriteGameStateArray[0];
    }

    public void GameLost()
    {
        _grid.RevealAllMines(); 
    }

    public void OnNormalClick()
    {
        gameStateImg.sprite = spriteGameStateArray[1];
    }

    public void OutClick()
    {
        gameStateImg.sprite = spriteGameStateArray[0];
    }
    //public void WinGameReward(GameMode mode)
    //{
    //    GameDataMode gameDataMode = GetGameDataModeFromGameMode(mode);

    //    int sum = gameDataMode.Mine + gameDataMode.coinReward;

    //    record.text = Math.Round(_grid.TimeCount, 3).ToString("0.000");
    //    bombFound.text = gameDataMode.Mine.ToString();
    //    winReward.text = gameDataMode.coinReward.ToString();
    //    coinReward.text = sum.ToString();

    //    Player.AddOrRemoveCoin(sum);
    //}

    public float TimeRecord()
    {
        return _grid.TimeCount;
    }

    public int MineFound()
    {
        return _grid.FoundMine();
    }

    public void LostGameReward()
    {
        int mineFoundLost = _grid.FoundMine();
        loseBombFound.text = mineFoundLost.ToString();
        lostCoinReward.text = mineFoundLost.ToString();

        Player.AddOrRemoveCoin(mineFoundLost);
    }

    public void HintMine()
    {
        _grid.Hint();
    }

    public void DisableGrid()
    {
        _grid.DisablePlayField();
    }

    public void EnableGrid()
    {
        _grid.EnablePlayField();
    }

    public void DisableTile()
    {
        _grid.DisableFieldTile();
    }

    public void EnableTile()
    {
        _grid.EnableFieldTile();
    }

    #endregion
}
