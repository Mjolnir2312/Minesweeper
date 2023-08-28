//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.EventSystems;
//public enum TileType
//{
//    Empty,
//    Normal,
//    Bomb,
//    Flag
//}
//public class Tile : MonoBehaviour
//{
//    #region Properties

//    public const int TILE_MINE = 9;
//    public const int TILE_HIGHLIGHT = 10;
//    public const int TILE_UNREVEALED = 11;
//    public const int TILE_FLAGGED = 12;
//    public const int TILE_FALSE_FLAG = 13;
//    public const int TILE_MINE_PRESSED = 14;
//    public const int TILE_FLAG_MODE = 15;
//    public const int TILE_QUESTION = 16;

//    private GridManager _grid;
//    private PlayerInput _playerInput;
//    private GameManager GM;


//    private Vector2 _gridPosition = Vector2.zero;   
//    private List<Vector2> _neighborTilePositions;
//    [SerializeField] private TileType type;
//    [SerializeField]
//    private int _tileValue;
//    private bool _flagmode;
//    private bool _revealed;                      
//    private bool _flagged;
//    private bool _question;
//    private bool _empty;


//    public Sprite[] Sprites;
//    public ModeGamePanel GamePanel;
//    #endregion

//    #region Get/Set
//    public Vector2 GridPosition
//    {
//        get { return _gridPosition; }
//        set { _gridPosition = value; }
//    }
//    public List<Vector2> NeighborTilePositions
//    {
//        get { return _neighborTilePositions; }
//        set { _neighborTilePositions = value; }
//    }
//    public GridManager Grid
//    {
//        get { return _grid; }
//        set { _grid = value; }
//    }
//    public int TileValue
//    {
//        get { return _tileValue; }
//        set
//        {
//            _tileValue = value;
//        }
//    }
//    #endregion

//    #region Unity Methods

//    void Awake()
//    {
//        _playerInput = GameObject.Find("GameManager").GetComponent<PlayerInput>();
//        GM = _playerInput.GetComponent<GameManager>();
//    }

//    void Start()
//    {
//        _flagged = false;
//        _revealed = false;
//        _flagmode = false;
//        _question = false;
//        _empty = false;
//    }

//    private void OnMouseDrag()
//    {
//        if (!GameManager.IsGamePaused)
//            _playerInput.OnMouseDrag(this);
//    }

//    private void OnMouseDown()
//    {
//        if (!GameManager.IsGamePaused)
//            _playerInput.OnMouseDown(this);
//    }

//    public void OnMouseUp()
//    {
//        if (!GameManager.IsGamePaused)
//            _playerInput.OnMouseUp(this);
//    }

//    #endregion

//    #region MAIN

//    public void SetTileType(TileType _type)
//    {
//        this.type = _type;
//    }
//    public void Reveal()
//    {
//        _revealed = true;
//        Debug.Log("Revealedddddddddddd");
//        if (this.IsMine())
//        {
//            GetComponent<SpriteRenderer>().sprite = Sprites[TILE_MINE_PRESSED];
//            GM.GameOver(false);
//        }
//        else
//        {
//            GetComponent<SpriteRenderer>().sprite = Sprites[_tileValue];
//            if (_tileValue == 0) RevealNeighbors();

//           // Debug.Log("TileValue: " + _tileValue);
//        }

//        if (_grid.AreAllTileTilesRevealed())
//        { 
//            GM.GameOver(true);
//        }
//    }
//    public void RevealNeighbors()
//    {
//        foreach (Vector2 pos in _neighborTilePositions)
//        {
//            Tile neighbor = _grid.Map[(int)pos.x][(int)pos.y];
//            if (!neighbor.IsRevealed() && !neighbor.IsFlagged())
//                neighbor.Reveal();
//        }
//    }

//    public void Conceal()
//    {
//        _revealed = false;
//        GetComponent<SpriteRenderer>().sprite = Sprites[TILE_UNREVEALED];
//    }

//    public void VirtualFlag()
//    {
//        _flagmode = true;
//        GetComponent<SpriteRenderer>().sprite = Sprites[TILE_FLAG_MODE];
//    }   
    
//    //public void Question()
//    //{
//    //    _flagged = true;
//    //    GetComponent<SpriteRenderer>().sprite = Sprites[TILE_QUESTION];
//    //}

//    public void PlaceMineOnTile()
//    {
//        _tileValue = TILE_MINE;
//    }

//    public void Highlight()
//    {
//        //_flagmode = false;
//        GetComponent<SpriteRenderer>().sprite = Sprites[TILE_HIGHLIGHT];
//    }

//    public void RevertHighlight()
//    {
//        GetComponent<SpriteRenderer>().sprite = Sprites[TILE_UNREVEALED];
//    }


//    public bool IsRevealed()
//    {
//        return _revealed;
//    }

//    public bool IsFlagged()
//    {
//        return _flagged;
//    }

//    public bool IsQuestion()
//    {
//        return _question;
//    }

//    public bool IsFastFlag()
//    {
//        return _flagmode;
//    }
//    public bool IsMine()
//    {
//        return _tileValue == TILE_MINE;
//    }

//    public bool IsEmpty()
//    {
//        return _empty;
//    }
//    public bool IsNeighborsFlagged()
//    {
//        int remaining_flags = _tileValue;

//        foreach (Vector2 pos in _neighborTilePositions)
//        {
//            Tile neighbor = _grid.Map[(int)pos.x][(int)pos.y];
//            if (neighbor.IsFlagged())
//            {
//                remaining_flags--;
//            }
//        }

//        return remaining_flags <= 0;
//    }

//    public void ToggleFlag()
//    {
//        _flagged = !_flagged;
//        GetComponent<SpriteRenderer>().sprite = _flagged ? Sprites[TILE_FLAGGED] : Sprites[TILE_UNREVEALED];
//        GM.UpdateFlagCounter(_flagged);
//    }

//    public void FastToFlag()
//    {
//        if( _flagged )
//        {
//            _flagmode = true;
//            _flagged = false;
//            GetComponent<SpriteRenderer>().sprite = Sprites[TILE_FLAG_MODE];
//            GM.UpdateFlagCounter(false);
//        }
//        else if(_flagmode)
//        {
//            _flagmode = false;
//            _flagged = true;
//            GetComponent<SpriteRenderer>().sprite = Sprites[TILE_FLAGGED];
//            GM.UpdateFlagCounter(true);
//        }
//        //GM.UpdateFlagCounter(_flagmode);
//    }

//    public void ChangeToQuestion()
//    {
//        if( _flagged )
//        {
//            _question = true;
//            _flagged = false;
//            GetComponent<SpriteRenderer>().sprite = Sprites[TILE_QUESTION];
//            GM.UpdateFlagCounter(false);
//        }
//        else if( _question )
//        {
//            _question = false;
//            _flagged = true;
//            GetComponent<SpriteRenderer>().sprite = Sprites[TILE_FLAGGED];
//        }
//    }    
//    #endregion
//}
