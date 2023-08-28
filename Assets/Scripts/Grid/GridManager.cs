//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.UI;
//using static UnityEditor.PlayerSettings;
//using Random = UnityEngine.Random;

//public class GridManager : MonoBehaviour
//{
//    #region Properties
//    public Tile TilePrefab;

//    private List<List<Tile>> _map = new List<List<Tile>>();
//    private GameObject[,] _tiles;
//    private GameSettings _settings;

//    [SerializeField] private float offsetSizeTile;

//    #endregion

//    #region Get
//    public List<List<Tile>> Map
//    {
//        get { return _map; }
//    }
//    #endregion

//    #region Unity Methods
//    private void Start()
//    {
//        //_settings = new GameSettings(9, 9, 10, "he");// hang, cot, bom
//        //GenerateGrid(_settings);
        
//    }
//    #endregion

//    #region MAIN

//    public void GenerateGrid(GameSettings Settings)
//    {
//        _settings = Settings;
//        _map = new List<List<Tile>>();
//        for (int _row = 0; _row < _settings.Height; _row++)
//        {
//            List<Tile> row = new List<Tile>();
//            for (int _col = 0; _col < _settings.Width; _col++)
//            {
//                float tileSizeX = 0.43f; // Scale grid
//                float tileSizeY = 0.43f;

//                float _width = _settings.Width * tileSizeX;
//                float _height = _settings.Height * tileSizeY;

//                float _startX = -_width / 2 + tileSizeX / 2;
//                float _startY = -_height / 2 + tileSizeY / 2;
//                float _x = _startX + _col * tileSizeY;
//                float _y = _startY + _row * tileSizeY;


//                Tile tile = (Instantiate<Tile>(TilePrefab, new Vector3(_x, _y, 0f), Quaternion.identity));
//                tile.GridPosition = new Vector2(_row, _col);
//                row.Add(tile);
//                tile.transform.parent = transform;
//                tile.GetComponent<Tile>().Grid = this;                
//            }
//            _map.Add(row);          
//        }

//      //  PlaceMinesOnTiles(TilePrefab);
//        UpdateTileValues();
//        //MoveCameraToGridCenter();
//    }


//    public void PlaceMinesOnTiles(Tile tile)
//    {
//        HashSet<Vector2> mineLocation = new HashSet<Vector2>();
//        for (int i = 0; i < _settings.Height; i++)
//        {
//            for (int j = 0; j < _settings.Width; j++)
//            {
//                mineLocation.Add(new Vector2(i, j));
//            }
//        }

//        int posX = (int)tile.GridPosition.x;
//        int posY = (int)tile.GridPosition.y;

//        for (int i = posX - 1; i <= posX + 1; i++)
//        {
//            for (int j = posY - 1; j <= posY + 1; j++)
//            {
//                mineLocation.Remove(new Vector2(i, j));
//            }
//        }

//        int minesPlaced = 0;
//        while (minesPlaced < _settings.Mines && mineLocation.Count > 0)
//        {
//            var r = mineLocation.ElementAt(Random.Range(0, mineLocation.Count));
//            Tile placedTile = _map[(int)r.x][(int)r.y];
//            placedTile.PlaceMineOnTile();
//            placedTile.SetTileType(TileType.Bomb);

//            Debug.Log("Tile Bomb: " + placedTile.GridPosition);

//            int NearByMineCount = 0;
//            foreach (Vector2 pos in placedTile.NeighborTilePositions)
//            {
//                Tile _tile = _map[(int)pos.x][(int)pos.y];
//                if (_tile.IsMine())
//                {
//                    ++NearByMineCount;
//                    // _tile.TileValue = NearByMineCount;
//                    //_tile.SetTileType(TileType.Normal);
//                }
//                //foreach (Vector2 pos in placedTile.NeighborTilePositions)
//                //{
//                //    Tile _tile = _map[(int)pos.x][(int)pos.y];
//                //    if (_tile.IsMine())
//                //    {
//                //        ++NearByMineCount;
//                //       // _tile.TileValue = NearByMineCount;
//                //        //_tile.SetTileType(TileType.Normal);
//                //    }
//                }

//                //  placedTile.TileValue = NearByMineCount;


//                mineLocation.Remove(r);
//            minesPlaced++;
//        }
//        //Debug.Log("MinesPlaced: " + minesPlaced);
//        minesPlaced = _settings.Mines;
//    }

//    void UpdateTileValues()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                SetNeighbors(tile);

//                if (!tile.IsMine())
//                {
//                    int NearByMineCount = 0;

//                    foreach (Vector2 pos in tile.NeighborTilePositions)
//                    {
//                        Tile _tile = _map[(int)pos.x][(int)pos.y];
//                        if (_tile.IsMine())
//                            ++NearByMineCount;
//                    }

//                    tile.TileValue = NearByMineCount;
//                }
//            }
//        }
//    }

//    void SetNeighbors(Tile tile) // SetNeighbors Around Number Or Mines
//    {
//        List<Vector2> NeighborPosition = new List<Vector2>();
//        NeighborPosition.Add(new Vector2(tile.GridPosition.x + 1, tile.GridPosition.y));
//        NeighborPosition.Add(new Vector2(tile.GridPosition.x - 1, tile.GridPosition.y));
//        NeighborPosition.Add(new Vector2(tile.GridPosition.x, tile.GridPosition.y - 1));
//        NeighborPosition.Add(new Vector2(tile.GridPosition.x, tile.GridPosition.y + 1));
//        NeighborPosition.Add(new Vector2(tile.GridPosition.x + 1, tile.GridPosition.y - 1));
//        NeighborPosition.Add(new Vector2(tile.GridPosition.x + 1, tile.GridPosition.y + 1));
//        NeighborPosition.Add(new Vector2(tile.GridPosition.x - 1, tile.GridPosition.y - 1));
//        NeighborPosition.Add(new Vector2(tile.GridPosition.x - 1, tile.GridPosition.y + 1));

//        for (int i = NeighborPosition.Count - 1; i >= 0; --i)
//        {
//            Vector2 pos = NeighborPosition[i];
//            if (pos.x < 0 || pos.x >= _settings.Height || pos.y < 0 || pos.y >= _settings.Width)
//            {
//                NeighborPosition.RemoveAt(i);
//            }
//        }

//        tile.NeighborTilePositions = NeighborPosition;
//    }

//    public void RevealAllTiles()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (!tile.IsMine())
//                    tile.Reveal();
//            }
//        }
//    }

//    public void ConcealAllTiles()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//                tile.Conceal();
//        }
//    }

//    public void RevealArea(Tile tile)
//    {
//        foreach (Vector2 neighborTilePos in tile.NeighborTilePositions)
//        {
//            Tile neighbor = _map[(int)neighborTilePos.x][(int)neighborTilePos.y];
//            if (!neighbor.IsRevealed() && !neighbor.IsFlagged())
//                neighbor.Reveal();
//        }
//    }

//    public void HighlightArea(Vector2 pos)
//    {
//        Tile tile = _map[(int)pos.x][(int)pos.y];

//        foreach (Vector2 _pos in tile.NeighborTilePositions)
//        {
//            Tile neighbor = _map[(int)_pos.x][(int)_pos.y];
//            if (!(neighbor.IsFlagged() || neighbor.IsRevealed()))
//                neighbor.Highlight();
//        }
//    }

//    public void HighlightTile(Vector2 pos)
//    {
//        Tile tile = _map[(int)pos.x][(int)pos.y];

//        if (!(tile.IsFlagged() || tile.IsRevealed()))
//            _map[(int)pos.x][(int)pos.y].Highlight();
//    }

//    public void RevertHighlightArea(Vector2 pos)
//    {
//        Tile tile = _map[(int)pos.x][(int)pos.y];

//        foreach (Vector2 _pos in tile.NeighborTilePositions)
//        {
//            Tile neighbor = _map[(int)_pos.x][(int)_pos.y];

//            if (!neighbor.IsFlagged() && !neighbor.IsRevealed())
//                neighbor.RevertHighlight();
//        }
//    }

//    public void RevertHighlightTile(Vector2 pos)
//    {
//        Tile tile = _map[(int)pos.x][(int)pos.y];
//        if (!tile.IsRevealed() && !tile.IsFlagged())
//            tile.RevertHighlight();
//    }

//    public void SwapTileWithMineFreeTile(Tile tile)
//    {
//        Tile mineFreeTile = null;
//        bool isFound = false;

     
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile t in row)
//            {
//                if (!t.IsMine() && t.TileValue == 0)
//                {
//                    mineFreeTile = t;
//                    isFound = true;
//                    break;
//                }
//            }
//            if (isFound) break;
//        }
//        Debug.Log("tile founded: " +  mineFreeTile.TileValue);

//        if (mineFreeTile != null)
//        {      
//            int tmp = mineFreeTile.TileValue;
//            mineFreeTile.TileValue = tile.TileValue;
//            tile.TileValue = tmp;
//        }
//        Debug.Log("tile value: " + tile.TileValue);
//        UpdateTileValues();
//    }

//    public bool AreAllTileTilesRevealed() // Open All Tiles Has Mine 
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (!tile.IsMine() && !tile.IsRevealed())
//                    return false;
//            }
//        }

//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (tile.IsMine() && !tile.IsFlagged())
//                    tile.ToggleFlag();
//            }
//        }
//        return true;
//    }

//    public void RevealMine()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (tile.IsMine() && !tile.IsFlagged() && !tile.IsRevealed())
//                {
//                    tile.GetComponent<SpriteRenderer>().sprite = tile.Sprites[9];

//                }    
//                if (!tile.IsMine() && tile.IsFlagged())
//                    tile.GetComponent<SpriteRenderer>().sprite = tile.Sprites[13];
//            }
//        }
//    }

//    public void FlagMode()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (!tile.IsRevealed() && !tile.IsFlagged())
//                    tile.VirtualFlag();
//            }
//        }       
//    } 

//    public void FlagModeGameOver()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (!tile.IsRevealed() && !tile.IsFlagged() && !tile.IsMine())
//                    tile.VirtualFlag();
//            }
//        }
//    }
    
//    public void OutFlagMode()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (!tile.IsRevealed() && tile.IsFastFlag() && !tile.IsFlagged())
//                {
//                    tile.RevertHighlight();
//                }
//            }
//        }
//    }

//    public void OutFlagModeGameOver()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (!tile.IsRevealed() && tile.IsFastFlag() && !tile.IsFlagged() && !tile.IsMine())
//                {
//                    tile.RevertHighlight();
//                }
//            }
//        }
//    }

//    public void ReaalAllTilesOnGameOver()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if(!tile.IsMine() && !tile.IsRevealed())
//                {
//                    tile.Reveal();
//                }
//            }
//        }
//    }

//    public void HintTileMine()
//    {
//        for (int x = 0; x < _map.Count; x++)
//        {
//            for (int y = 0; y < _map[x].Count; y++)
//            {
//                Tile tile = _map[x][y];
//                if (tile.IsMine() && !tile.IsFlagged() && !tile.IsRevealed())
//                {
//                    tile.ToggleFlag();
//                    return;
//                }
//            }
//        }
//    }

//    public void AddLife()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (tile.IsMine() && tile.IsRevealed() && !tile.IsFlagged())
//                {
//                    tile.ToggleFlag();
//                }
//            }
//        }
//    } 
    
//    public void UnRevealed()
//    {
//        foreach (List<Tile> row in _map)
//        {
//            foreach (Tile tile in row)
//            {
//                if (tile.IsMine() && !tile.IsRevealed() && !tile.IsFlagged())
//                {
//                    tile.RevertHighlight();
//                }
//            }
//        }
//    }

//    public void OpenRandomTile()
//    {
//        int rowIndex = Random.Range(0, _map.Count);
//        int colIndex = Random.Range(0, _map[rowIndex].Count);

//        Tile randomTile = _map[rowIndex][colIndex];
//        randomTile.OnMouseUp();
//    }

//    #endregion
//}