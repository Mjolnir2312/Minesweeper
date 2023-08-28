using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GridControl : MonoBehaviour
{
    public enum EGameState
    {
        Uninitialized,
        Playing,
        Win,
        Lose
    }

    #region Properties
    public float MinScale, MaxScale;

    public TileScript TilePrefab;
    public GameObject CameraGrid;
    public GameDataMode GameDataSettingsMode;

    //public DataSetting GameData;

    private TileScript[,] _tiles;

    private int _numRevealedTiles;
    public int _numBombLeft;
    private float _startTime;

    private bool _updateTime;
    private bool _checkWin;
    private bool _createGame;
    private Vector2i firstTilePos;

    public int ShowTime;
    public float TimeCount;

    //private GameSettings settings;
    private EGameState _gameState = EGameState.Uninitialized;
    private GameManager GM;

    [SerializeField] private float upGrid;
    [SerializeField] private Text mineCount;
    [SerializeField] private Text timeCount;
    #endregion

    #region Get/Set
    public EGameState GameState
    {
        set
        {
            _gameState = value;
            OnGameStateChange();
        }
        get { return _gameState; }
    }

    public int NumBombsLeft
    {
        get { return _numBombLeft; }
        set { _numBombLeft = value; }
    }
    #endregion

    #region Unity Core
    public Rect VisibleField
    {
        get
        {
            var cam = CameraGrid.GetComponent<Camera>();
            var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            return new Rect(bottomLeft, topRight - bottomLeft);
        }
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {

    }
    private void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //_settings = GameSettings.Easy;
    }
    // Start is called before the first frame update
    void Start()
    {
        //CameraMain.GetComponent<Camera>().Reset();
        //AutoOpenRandomTile();
        //StartNewGame(GameDataSettingsMode);
    }

    // Update is called once per frame
    void Update()
    {
        //AutoOpenRandomTile();
        mineCount.text = _numBombLeft.ToString();

        if (_updateTime)
        {
            TimeCount = Time.time - _startTime;
            ShowTime = (int)(Time.time - _startTime);
            timeCount.text = ShowTime.ToString();
        }

    }
    #endregion

    #region Main
    public void PlaceMines()
    {
        PlaceMines(-1, -1);
    }

    public void PlaceMines(int posX, int posY)
    {
        HashSet<Vector2i> tileList = new HashSet<Vector2i>(new Vector2iComparer());
        for (int i = 0; i < GameDataSettingsMode.Width; i++)
        {
            for (int j = 0; j < GameDataSettingsMode.Height; j++)
            {
                tileList.Add(new Vector2i(i, j));
            }
        }

        if (posX >= 0 && posY >= 0)
        {
            for (int i = posX - 1; i <= posX + 1; i++)
            {
                for (int j = posY - 1; j <= posY + 1; j++)
                {
                    tileList.Remove(new Vector2i(i, j));
                }
            }
        }

        int minesPlaced = 0;
        while (minesPlaced < GameDataSettingsMode.Mine && tileList.Count > 0)
        {
            var r = tileList.ElementAt(Random.Range(0, tileList.Count));
            var tile = _tiles[r.X, r.Y].GetComponent<TileScript>();
            tile.Type = TileScript.EType.Mine;

            foreach (var neighbour in tile.Neighbours)
            {
                neighbour.Number++;
            }

            tileList.Remove(r);
            minesPlaced++;
        }

        //_updateTime = true;
        GameDataSettingsMode.Mine = minesPlaced;
        _numBombLeft = minesPlaced;
        GameState = EGameState.Playing;
        StartTimer();
    }


    public void CreateTile(GameDataMode Settings)
    {
        GameDataSettingsMode = Settings;

        var tileRenderSize = TilePrefab.GetComponent<Renderer>().bounds.size;
        var fieldRenderSize = new Vector3(GameDataSettingsMode.Width * tileRenderSize.x, GameDataSettingsMode.Height * tileRenderSize.y);
        var cameraWorldViewSize = VisibleField;

        var scale = Mathf.Clamp(Math.Min(cameraWorldViewSize.width / fieldRenderSize.x,
                                         cameraWorldViewSize.height / fieldRenderSize.y),
                                         MinScale, MaxScale);

        tileRenderSize *= scale;
        fieldRenderSize *= scale;



        _tiles = new TileScript[GameDataSettingsMode.Width, GameDataSettingsMode.Height];

        for (int i = 0; i < GameDataSettingsMode.Width; i++)
        {
            for (int j = 0; j < GameDataSettingsMode.Height; j++)
            {
                _tiles[i, j] = Instantiate(TilePrefab,
                            new Vector3((i + .5f) * tileRenderSize.x - fieldRenderSize.x / 2f, -j * tileRenderSize.y + GameDataSettingsMode.UpGrid),
                            new Quaternion());


                var tile = _tiles[i, j];
                tile.name = $"Tile[{i}, {j}]";
                tile.transform.parent = transform;
                tile.Parent = this;
                tile.Container = _tiles;
                tile.GridPos = new Vector2i(i, j);
                tile.LocalScale = scale;
                _createGame = true;
            }
        }

        for (int i = 0; i < GameDataSettingsMode.Width; i++)
        {
            for (int j = 0; j < GameDataSettingsMode.Height; j++)
            {
                for (int ii = i - 1; ii <= i + 1; ii++)
                {
                    for (int jj = j - 1; jj <= j + 1; jj++)
                    {
                        if (i == ii && j == jj)
                            continue;
                        try
                        {
                            _tiles[i, j].NewNeighbour = _tiles[ii, jj];
                        }
                        catch (IndexOutOfRangeException) { }
                    }
                }
            }
        }
        _numBombLeft = GameDataSettingsMode.Mine;
    }
    #endregion

    #region Help

    #region Game State
    public void CheckInitialization(int posX, int posY)
    {
        if (_gameState == EGameState.Uninitialized)
            PlaceMines(posX, posY);
    }

    public void SetLoseState()
    {
        _gameState = EGameState.Lose;
        RevealAllMines();
    }

    public void StartNewGame(GameDataMode settings)
    {
        GameDataSettingsMode = settings;
        CreateTile(settings);
        _gameState = EGameState.Uninitialized;
        _numRevealedTiles = 0;
        NumBombsLeft = GameDataSettingsMode.Mine;
        ResetTimer();
        //GameStateButton.sprite = GameStateButton.GetComponent<StateButton>().Default;
        GamePlaySettings.isFlagMode = false;
        WhenAutoOpenSet();
    }

    public void Restart()
    {
        foreach (TileScript tile in _tiles)
        {
            Destroy(tile.gameObject);
        }
        CreateTile(GameDataSettingsMode);
        _gameState = EGameState.Uninitialized;
        _numRevealedTiles = 0;
        NumBombsLeft = GameDataSettingsMode.Mine;
        ResetTimer();
        //GameStateButton.sprite = GameStateButton.GetComponent<StateButton>().Default;
        GamePlaySettings.isFlagMode = false;
        WhenAutoOpenSet();
    }

    public void DisablePlayField()
    {
        StopTimer();
        foreach (var tile in _tiles)
        {
            tile.GetComponent<TileScript>().enabled = false;
            tile.GetComponent<TileScript>().Enabled = false;
        }
    }

    public void DisableFieldTile()
    {
        foreach (TileScript tile in _tiles)
        {
            tile.enabled = false;
            tile.Enabled = false;
        }
    }

    public void EnableFieldTile()
    {
        foreach (var tile in _tiles)
        {
            tile.Enabled = true;
            tile.enabled = true;
        }
    }


    public void EnablePlayField()
    {

        if (_gameState == EGameState.Playing || _gameState == EGameState.Uninitialized)
        {
            ResumeTimer();
            foreach (var tile in _tiles)
            {
                tile.GetComponent<TileScript>().Enabled = true;
                tile.GetComponent<TileScript>().enabled = true;
            }
        }
    }

    public void UpdateFlagCounter(bool condition)
    {
        _numBombLeft += condition ? -1 : 1;
    }


    private void OnGameStateChange()
    {
        switch (_gameState)
        {
            case EGameState.Win:
                FlagWhenWin();
                DisablePlayField();
                GM.GameOver(true);
                break;
            case EGameState.Lose:
                //RevealAllMines();
                DisablePlayField();
                GM.GameOver(false);
                break;

        }
    }

    public void ContinueWhenLose()
    {

        GameState = EGameState.Playing;
        ResumeTimer();


        ChangeMineToFlag();
        UpdateFlagCounter(true);

        EnablePlayField();

    }

    private void CheckLostGame()
    {

    }
    private void ChangeMineToFlag()
    {
        foreach (TileScript tile in _tiles)
        {
            tile.AddFlag();
        }
    }
    #endregion
    #region Timer
    private void StartTimer()
    {
        _startTime = Time.time;
        ResetTimer();
    }

    private void ResumeTimer()
    {
        _startTime += (int)(Time.time - (_startTime + ShowTime));
        _updateTime = true;
    }
    public void StopTimer()
    {
        ShowTime = (int)(Time.time - _startTime);
        _updateTime = false;
    }

    private void ResetTimer()
    {
        ShowTime = 0;
        timeCount.text = "0";
        _updateTime = false;
        mineCount.text = "0";
    }
    #endregion

    #region Tile Events
    public void OnTileClick(TileScript tile)
    {
        if (_gameState == EGameState.Uninitialized)
        {
            PlaceMines(tile.GridPos.X, tile.GridPos.Y);
            _updateTime = true;
            //Debug.Log(tile);

            firstTilePos = new Vector2i(tile.GridPos.X, tile.GridPos.Y);
        }

        if (_gameState != EGameState.Playing)
            return;

        tile.Revealed = true;

        if (tile.Revealed && tile.Type == TileScript.EType.Mine)
        {

            GameState = EGameState.Lose;
            tile.SetAsDeadMine();
            SoundManager.Instance.MineSound();
        }

        if (AreAllTileTilesRevealed())
        {
            GameState = EGameState.Win;
        }
    }


    public void OnTileReveal(TileScript tile)
    {
        _numRevealedTiles++;
    }

    public void DestroyTiles()
    {
        if (_tiles != null)
        {
            foreach (TileScript tile in _tiles)
            {
                if (tile != null)
                {
                    Destroy(tile.gameObject);
                }
            }
            _tiles = null;
        }
    }

    public void OnTileFlagClick(TileScript tile)
    {
        tile.Flagged = !tile.Flagged;
        UpdateFlagCounter(tile.Flagged);
    }

    public void FlagWhenWin()
    {
        foreach (TileScript tile in _tiles)
        {
            tile.RevealAsWinState();
        }
    }

    public void RevealAllMines()
    {
        foreach (TileScript tile in _tiles)
        {
            tile.RevealAsLoseState();
        }
    }

    public void AutoOpenRandomTile()
    {
        int randomX = Random.Range(0, GameDataSettingsMode.Width);
        int randomY = Random.Range(0, GameDataSettingsMode.Height);

        TileScript randomTile = _tiles[randomX, randomY];
        if (_gameState == EGameState.Uninitialized)
        {
            OnTileClick(randomTile);
        }
    }

    public void Hint()
    {
        if (_gameState == EGameState.Playing)
        {
            bool foundMine = false;

            for (int y = 0; y < GameDataSettingsMode.Height; y++)
            {
                for (int x = 0; x < GameDataSettingsMode.Width; x++)
                {
                    TileScript tile = _tiles[x, y];
                    if (tile.Type == TileScript.EType.Mine && !tile.Revealed && !tile.Flagged)
                    {
                        tile.FlagSwap();
                        foundMine = true;
                        break; 
                    }
                }

                if (foundMine)
                {
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Start The Game");
        }
    }

    public void WhenAutoOpenSet()
    {
        bool AutoOpen = GamePlaySettingsUI.isAutoOpen;

        if (AutoOpen)
        {
            Invoke("AutoOpenRandomTile", 0.01f);
        }
    }

    public void OnFlagMode()
    {
        bool isFlagMode = GamePlaySettings.isFlagMode;

        foreach (TileScript tile in _tiles)
        {
            if (isFlagMode)
            {
                tile.FlagMode();
            }
            else
            {
                tile.UnFlagMode();
            }
        }
    }

    public int FoundMine()
    {
        int bomFound = 0;
        foreach (TileScript tile in _tiles)
        {
            if (tile.Type == TileScript.EType.Mine && tile.Flagged)
            {
                bomFound++;
            }
        }

        return bomFound;
    }

    public bool AreAllTileTilesRevealed() // Open All Tiles Has Mine 
    {

        foreach (TileScript tile in _tiles)
        {
            if (tile.Type == TileScript.EType.Safe && !tile.Revealed)
            {
                return false;
            }
        }

        //foreach (TileScript tile in _tiles)
        //{
        //    if(tile.Type == TileScript.EType.Mine && !tile.Flagged)
        //    {

        //    }
        //}
        return true;
    }
    #endregion
    #endregion
}

