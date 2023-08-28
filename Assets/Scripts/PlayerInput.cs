//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class PlayerInput : MonoBehaviour
//{
//    private static bool _revealAreaIssued;
//    private static bool _initialClickIssued;
//    private bool _flag;
//    private bool holdFlag = true; //hold to flag or not

//    private GridManager _grid;
//    public UIManager UI;
//    public Canvas PauseMenu;
//    public Image StateButton;

//    float countTime = 0.0f;
//    float flagTime = 0.5f;

//    [SerializeField] private AudioClip _tickClip;

//    public GridManager Grid
//    {
//        get { return _grid; }
//        set { _grid = value; }
//    }

//    public static bool InitialClickIssued
//    {
//        get { return _initialClickIssued; }
//        set { _initialClickIssued = value; }
//    }

//    private void Awake()
//    {
//        Vibration.Init();
//    }
//    //private void Update()
//    //{
//    //    if(Input.GetKeyDown(KeyCode.F))
//    //    {
//    //        holdFlag = false;
//    //    }
//    //    //Debug.Log(holdFlag);
//    //}

//    public void OnMouseDrag(Tile tile)
//    {
//        if (countTime < flagTime)
//        {
//            countTime += Time.deltaTime;
//        }
//        else if (!_flag && !tile.IsRevealed())
//        {
//            _flag = true;
//            tile.ToggleFlag();
//            bool vibration = UISettings.isVibrationOn;

//            if (vibration)
//            {
//                Vibration.VibratePop();
//            }
//        }
//    }
//    public void OnMouseDown(Tile tile)
//    {
//        StateButton.sprite = StateButton.GetComponent<StateButton>().Clicked;
//    }

//    public void OnMouseUp(Tile tile)
//    {
//        countTime = 0;
//        StateButton.sprite = StateButton.GetComponent<StateButton>().Default;
//        SoundManager.Instance.PlaySound(_tickClip);

//        if (_flag && holdFlag == true) 
//        {
//            _flag = false;        
//        }
//        else 
//            PerformMouseDown(tile);
//    }

//    private void PerformMouseDown(Tile tile)
//    {
//        bool vibration = UISettings.isVibrationOn;
//        if (vibration)
//        {
//            Vibration.VibratePop();
//        }
                  

//        bool FlagMode = GamePlaySettings.isFlagMode;

//        if (FlagMode)
//        {
//            if (!tile.IsRevealed())
//            {
//                tile.FastToFlag();
//            }
//        }
//        else
//        {
//            OpenTile(tile);
//        }    
//    }

//    public void OpenTile(Tile tile)
//    {
//        if (!tile.IsRevealed() && !tile.IsFlagged())
//        {
//            //_grid.HighlightTile(tile.GridPosition);

//            //foreach (Vector2 pos in tile.NeighborTilePositions)
//            //{
//            //    Tile neighbor = _grid.Map[(int)pos.x][(int)pos.y];
//            //    if (!neighbor.IsRevealed() && !neighbor.IsFlagged() && !neighbor.IsQuestion())
//            //    {
//            //        neighbor.RevertHighlight();
//            //    }
//            //}

//            if (!_initialClickIssued)
//            {
//                Grid.PlaceMinesOnTiles(tile);
//                //if (tile.TileValue != 0)
//                //{
//                //    Debug.Log(tile.TileValue);
//                //    _grid.SwapTileWithMineFreeTile(tile);
//                //    //tile = _grid.Map[(int)tile.GridPosition.x][(int)tile.GridPosition.y];
//                //}   
//                Debug.Log("TIle: " + tile.GridPosition);
//                tile.Reveal();

//                //Debug.Log("_initialClickIssued is true: " + tile.TileValue);
//                _initialClickIssued = true;
//                //GetComponent<GameManager>().StartTimer();
//            }
//            else
//                tile.Reveal();
//        }

//        //if (!tile.IsRevealed() && !tile.IsFlagged())
//        //{
//        //    tile.RevertHighlight();
//        //}


//        //if (!tile.IsRevealed() && tile.IsFlagged())
//        //{
//        //    tile.ChangeToQuestion();
//        //}
//    }

//}
