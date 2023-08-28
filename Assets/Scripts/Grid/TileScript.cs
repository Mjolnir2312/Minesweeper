using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static TileScript;

public class TileScript : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EType
    {
        Safe,
        Mine
    }

    public enum Enumber
    {
        Empty,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight
    }
    #region Fields

    public Sprite TileUnknow, TilePressed;
    public Sprite Tile0, Tile1, Tile2, Tile3, Tile4, Tile5, Tile6, Tile7, Tile8;
    public Sprite TileMine, TileFlag, TileQuestion, TileFlagMode;
    public Sprite TileDead, TileWrong;

    private Sprite _sprRevealed;


    public static bool OnClickTile;

    private EType _type = EType.Safe;

    private Enumber _num = Enumber.Empty;

    private bool _enabled = true;
    private bool _flagged;
    private bool _revealed;
    private bool _flaggedMode;
    private bool _question;
    private bool holdFlag = true; 
    private bool _flag;

    [SerializeField]
    private SpriteRenderer _sprRend;
    private GridControl _parent;
    private TileScript[,] _container;
    private Vector2i _gridPos;
    private List<TileScript> _neighbours = new List<TileScript> ();

    [SerializeField] private AudioClip _tickClip;

    float countTime = 0.0f;
    float flagTime = 0.5f;
    public float LocalScale = 1;


    #endregion

    public EType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            AssignTexture();
        }
    }

  
    public Enumber Number
    {
        get { return _num; }
        set
        {
            _num = value;
            AssignTexture();
        }
    }


    public GridControl Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    public TileScript[,] Container
    {
        get { return _container; }
        set { _container = value; }
    }

    public Vector2i GridPos
    {
        get { return _gridPos; }
        set { _gridPos = value; }
    }


    public bool Revealed
    {
        get { return _revealed; }
        set
        {
            if (_revealed && !value)
            {
                Unreveal();
                _revealed = false;
            }
            else if (!_revealed && value)
            {
                Reveal();
            }
        }
    }

    public bool Flagged
    {
        get { return _flagged; }
        set
        {
            if (value)
                Flag();
            else
                Unflag();
        }
    }

    public List<TileScript> Neighbours
    {
        get { return _neighbours; }
    }

    public TileScript NewNeighbour
    {
        set { _neighbours.Add(value); }
    }

    public bool Enabled
    {
        get { return _enabled; }
        set { _enabled = value; }
    }

    void Awake()
    {
        _sprRevealed = Tile0;
        Vibration.Init();
    }

    void Start()
    {
        _sprRend = GetComponent<SpriteRenderer>();
        _sprRend.sprite = TileUnknow;
        transform.localScale = new Vector3(LocalScale, LocalScale, LocalScale);
    }

    void Update()
    {
        holdFlag = GamePlaySettingsUI.isHolding;
    }

    private void OnMouseDrag()
    {
        if (!_enabled)
            return;

        if (countTime < flagTime)
        {
            countTime += Time.deltaTime;
        }
        else if (!_revealed && !_flag && holdFlag)
        {
            _flag = true;
            _parent.OnTileFlagClick(this);

            SoundManager.Instance.TickSound();         
            GameManager.Instance.OutClick();

            bool vibration = UISettings.isVibrationOn;

            if (vibration)
            {
                Vibration.VibratePop();
            }
        }
    }
    private void OnMouseDown()
    {
        if (!_enabled)
        {
            return;
        }
            GameManager.Instance.OnNormalClick();
    }

    private void OnMouseUp()
    {
        if (!_enabled)
        {
            return;
        }

        countTime = 0;

        if(_flag)
        {
            _flag = false;
        }
        else
        {
            OnTile();
        }

    }

    void OnTile()
    {
        SoundManager.Instance.TickSound();
        GameManager.Instance.OutClick();

        bool vibration = UISettings.isVibrationOn;

        if (vibration)
        {
            Vibration.VibratePop();
        }

        bool isFlagMode = GamePlaySettings.isFlagMode;
        if (isFlagMode) 
        {
            _flaggedMode = !_flaggedMode;

            if (_flaggedMode && !_revealed)
            {
                _flagged = false;
                UpdateSprite(TileFlagMode);
                _parent.UpdateFlagCounter(false);
            }
            else if (_flagged && !_flaggedMode)
            {
                _flagged = false;
                UpdateSprite(TileFlagMode);
                _flaggedMode = true;
                _parent.UpdateFlagCounter(false);
            }
            else if(!_flagged && !_revealed)
            {
                _flagged = true;
                UpdateSprite(TileFlag);
                _parent.UpdateFlagCounter(true);
            }
        }
        else
        {
            if (!_revealed && !_flagged)
            {
                TilePress();
                _parent.OnTileClick(this);
            }

            if (_question)
            {
                ReturnQuestion();
            }
            else if(!_question && _flagged)
            {
                Question();
            }
        }    
    }    

    public void UpdateSprite(Sprite sprite)
    {
        _sprRend.sprite = sprite;
    }    
    public void ClearNeighbours()
    {
        _neighbours.Clear();
    }

    public void Reveal()
    {
        // skip if already revealed or flagged
        if (_revealed || _flagged)
            return;

        _revealed = true;
        _sprRend.sprite = _sprRevealed;
        if (_type != EType.Mine && _num == Enumber.Empty)
            Neighbours.ForEach(neighbour => neighbour.Revealed = true);

        _parent.OnTileReveal(this);
    }

 
    public void SetAsDeadMine()
    {
        UpdateSprite(TileDead);
    }



    public void Unreveal()
    {    
        if (!_revealed)
            return;

        _revealed = false;
        UpdateSprite(TileUnknow);
    }


    public void RevealAsLoseState()
    {
        if (_type == EType.Mine && !_flagged && !_revealed)
        {
            UpdateSprite(TileMine);
        }
        else if (_type != EType.Mine && _flagged)
        {
            UpdateSprite(TileWrong);
        }
    }

    public void AddFlag()
    {
        if (_type == EType.Mine && !_flagged && _revealed)
        {
            //_sprRend.sprite = TileMine;
            UpdateSprite(TileFlag);
        }
        else if(_type == EType.Mine && !_flagged && !_revealed)
        {
            UpdateSprite(TileUnknow);
        }
    }
    public void RevelAsLoseFlagMode()
    {
        if (_type == EType.Mine && !_flagged && !_revealed)
        {
            //_sprRend.sprite = TileMine;
            UpdateSprite(TileMine);
        }
    }    

    public void RevealAsWinState()
    {
        if(_type == EType.Mine && !_flagged && !_revealed)
        {
            UpdateSprite(TileFlag);
        }
    }


    public void FlagSwap()
    {
        if (_flagged)
        {
            _parent.UpdateFlagCounter(false);
            Unflag();
        }
        else
        {
            Flag();
            _parent.UpdateFlagCounter(true);
        }
    }


    public void Flag()
    {      
        if (_flagged || _revealed)
            return;

        _flagged = true;
        //_sprRend.sprite = TileFlag;
        UpdateSprite(TileFlag);
    }


    public void Unflag()
    {     
        if (!_flagged)
            return;

        _flagged = false;
        //_sprRend.sprite = TileUnknow;
        UpdateSprite(TileUnknow);
    }


    public void TilePress()
    {
        if (!_revealed && !_flagged)
        {
            //_sprRend.sprite = TilePressed;
            UpdateSprite(TilePressed);
        }
    }

    public void Question()
    {
        if(_flagged && !_revealed)
        {
            UpdateSprite(TileQuestion);
            _question = true;
            _parent.UpdateFlagCounter(false);
        }
    }

    public void ReturnQuestion()
    {
        if (_question)
        {
            UpdateSprite(TileFlag);
            _question = false;
            _parent.UpdateFlagCounter(true);
        }
    }


    public void FlagMode()
    {
        if(!_revealed && !_flagged)
        {
            //_sprRend.sprite = TileFlagMode;
            UpdateSprite(TileFlagMode);
        }

        //Debug.Log("Revealed: " + _revealed);
        _flaggedMode = true;
    }    

    public void UnFlagMode()
    {
        if (_flaggedMode && !_revealed && !_flagged)
        {
            //UpDateSprite(TileUnknow);
            //_sprRend.sprite = TileUnknow;
            UpdateSprite(TileUnknow);
            _flaggedMode = false;
        }
        return;
    }
    
    public void ChangeToFlag()
    {
        if(!_flaggedMode && !_revealed && !_flagged)
        {
            _flagged = true;
            UpdateSprite(TileFlag);
        }
    }    

    public void ReturnFlagMode()
    {
        if (!_flaggedMode && !_revealed && !_flagged)
        {
            _flagged = false;
            UpdateSprite(TileFlagMode);
        }
    }

    private void TileRelease()
    {
        if (!_revealed && !_flagged)
            UpdateSprite(TileUnknow);
    }

    public void ResetToDefaultState()
    {
        _type = EType.Safe;
        _num = Enumber.Empty;
        _flagged = false;
        _revealed = false;
        _flaggedMode = false;
        _question = false;
        _flag = false;
        AssignTexture();
        UpdateSprite(TileUnknow);
    }

    private void AssignTexture()
    {
        switch (_type)
        {
            case EType.Safe:
                switch (_num)
                {
                    case Enumber.Empty:
                        _sprRevealed = Tile0;
                        break;
                    case Enumber.One:
                        _sprRevealed = Tile1;
                        break;
                    case Enumber.Two:
                        _sprRevealed = Tile2;
                        break;
                    case Enumber.Three:
                        _sprRevealed = Tile3;
                        break;
                    case Enumber.Four:
                        _sprRevealed = Tile4;
                        break;
                    case Enumber.Five:
                        _sprRevealed = Tile5;
                        break;
                    case Enumber.Six:
                        _sprRevealed = Tile6;
                        break;
                    case Enumber.Seven:
                        _sprRevealed = Tile7;
                        break;
                    case Enumber.Eight:
                        _sprRevealed = Tile8;
                        break;
                }
                break;
            case EType.Mine:
                _sprRevealed = TileMine;
                break;

        }
    }
}
