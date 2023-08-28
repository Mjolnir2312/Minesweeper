//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;

//public class UIManager : MonoBehaviour
//{
//    #region Properties
//    public GameManager GameManager;
//    public StateButton StateButton;
//    public Text CustomText;
//    public Text CustomMineText;
//    public Camera MainCamera;

//    private float defaulCameraSize;
//    private float sizeCameraWithMode;
//    private bool _isCustom;
//    private GameSettings _UI_GameSettings;

//    [SerializeField] private MenuElements _menuElements;
//    [SerializeField] private HUDElements _hUDElements;

//    [Header("UI GamePlay")]
//    [SerializeField] private GameObject modeGame;

//    [SerializeField] private Image easyTickImage;
//    [SerializeField] private Image mediumTickImage;
//    [SerializeField] private Image hardTickImage;
//    [SerializeField] private Image extremeTickImage;
//    [SerializeField] private Image customTickImage;
//    #endregion

//    #region Get/Set
//    public MenuElements MenuElements
//    {
//        get { return _menuElements; }
//    }

//    public HUDElements HUDElements
//    {
//        get { return _hUDElements; }
//        set { _hUDElements = value; }
//    }
//    #endregion

//    #region Unity Methods

//    private void OnEnable()
//    {

//    }

//    private void Awake()
//    {
//        defaulCameraSize = MainCamera.orthographicSize;
//    }
//    private void Start()
//    {
//        //_UI_GameSettings = GameManager.Settings;
//        WriteSettingsToInputText(_UI_GameSettings);

//        _menuElements.RowSlider.onValueChanged.AddListener(OnHeightSliderValueChanged);
//        _menuElements.ColSlider.onValueChanged.AddListener(OnWidthSliderValueChanged);
//    }

//    private void Update()
//    {
//        if(_isCustom)
//        {
//            CustomText.text = +_menuElements.RowSlider.value + "x" + _menuElements.ColSlider.value;
//            CustomMineText.text = _menuElements.MinesSlider.value.ToString();
//        }
//    }
//    #endregion

//    #region MAIN
//    public void OptionButtonClicked(string option)
//    {
//        easyTickImage.enabled = false;
//        mediumTickImage.enabled = false;
//        hardTickImage.enabled = false;
//        extremeTickImage.enabled = false;
//        customTickImage.enabled = false;

//        switch (option)
//        {
//            case "Easy":
//                SetLevel(GameSettings.Easy);
//                sizeCameraWithMode = defaulCameraSize;
//                easyTickImage.enabled = true;
//                break;

//            case "Medium":
//                mediumTickImage.enabled = true;
//                sizeCameraWithMode = 8;
//                SetLevel(GameSettings.Medium);
//                break;

//            case "Hard":
//                hardTickImage.enabled = true;
//                sizeCameraWithMode = 14;
//                SetLevel(GameSettings.Hard);
//                break;

//            case "Extreme":
//                extremeTickImage.enabled = true;
//                sizeCameraWithMode = 14;
//                SetLevel(GameSettings.Extreme);
//                break;

//            case "Custom":
//                customTickImage.enabled = true;
//                SetCustomSetting(true);
//                break;
//        }

//        WriteSettingsToInputText(_UI_GameSettings);
//    }


//    public void CustomButtonClicked()
//    {
//        SetLevel(GameSettings.Easy);
//    }

//    public void SetLevel(GameSettings settings)
//    {
//        _UI_GameSettings = settings;
//        WriteSettingsToInputText(_UI_GameSettings);
//    }

//    public void StartGameButtonClicked()
//    {
//        GameSettings settings = ReadSettings();

//        if(settings.isValid())
//        {
//            GameManager.StartNewGame(settings);
//        }
//    }

//    void SetCustomSetting(bool isCustom)
//    {
//        _isCustom = isCustom;

//        _menuElements.RowSlider.interactable    = isCustom;
//        _menuElements.ColSlider.interactable    = isCustom;
//        _menuElements.MinesSlider.interactable  = isCustom;
//    }

//    public GameSettings ReadSettings()
//    {
//        if(_isCustom)
//        {
//            int w = (int)_menuElements.RowSlider.value;
//            int h = (int)_menuElements.ColSlider.value;
//            int m = (int)_menuElements.MinesSlider.value;

//            _UI_GameSettings = new GameSettings(w, h, m, "custom");
//        }

//        return _UI_GameSettings;
//    }

//    void WriteSettingsToInputText(GameSettings settings)
//    {
//        if(settings == null)
//        {
//            return;
//        }

//        _menuElements.RowSlider.value   = settings.Width;
//        _menuElements.ColSlider.value   = settings.Height;
//        _menuElements.MinesSlider.value = settings.Mines;
//    }
    
//    public void DisableScoreCanvas()
//    {
        
//    }

//    public void GameOverButton()
//    {
//        GameManager.GameOver(true);
//    }

//    public void UpdateTimeText(int time)
//    {
//        _hUDElements.TimerText.text = "";
//        if(time < 10)
//        {
//            _hUDElements.TimerText.text += "00" + time;
//        }
//        else if(time < 100)
//        {
//            _hUDElements.TimerText.text += "0" + time;
//        }
//        else if(time < 1000)
//        {
//            _hUDElements.TimerText.text += time.ToString();
//        }
//    }

//    public void UpdateFlagText(int flagCount)
//    {
//        _hUDElements.FlagText.text = "";

//        bool isNegative = flagCount < 0;

//        string flagCountText = Mathf.Abs(flagCount).ToString();

//        int numberOfZeros = 3 - flagCountText.Length;

//        if (isNegative)
//        {
//            flagCountText = "-" + flagCountText;
//        }
//        else
//        {
//            for (int i = 0; i < numberOfZeros; i++)
//            {
//                flagCountText = "0" + flagCountText;
//            }
//        }

//        _hUDElements.FlagText.text += flagCountText;
//    }

//    public void RestartButton()
//    {
//        GameManager.StartNewGame(GameManager.Settings);
//    }
//    public void NewGameButton()
//    {
//        GameSettings settings = ReadSettings();

//        if(settings.isValid())
//        {
//            GameManager.StartNewGame(settings);
//            MainCamera.orthographicSize = sizeCameraWithMode;
//        }
//        else
//        {
//            Debug.Log("Error");
//        }
//    }
    
//    public void OnHeightSliderValueChanged(float value)
//    {
//        UpdateMinesSliderMaxValue();
//        UpdateCustomText();
//    }

//    public void OnWidthSliderValueChanged(float value)
//    {
//        UpdateMinesSliderMaxValue();
//        UpdateCustomText();
//    }

//    public void ResetHUD(int flagCount)
//    {
//        HUDElements.GameState.enabled = false;
//        UpdateFlagText(flagCount);
//        UpdateTimeText(0);
//    }

//    void UpdateCustomText()
//    {
//        int height = (int)_menuElements.RowSlider.value;
//        int width = (int)_menuElements.ColSlider.value;      
//    }

//    private void UpdateMinesSliderMaxValue()
//    {
//        int height = (int)_menuElements.RowSlider.value;
//        int width = (int)_menuElements.ColSlider.value;

//        int emptyCellsCount = height * width;

//        int maxMines = (int)(emptyCellsCount * 0.35f);

//        _menuElements.MinesSlider.maxValue = maxMines;
//    }

//    #endregion
//}
