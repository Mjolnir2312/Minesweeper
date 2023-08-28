using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSettingMode : MonoBehaviour
{
    [SerializeField] private Text _lineText;
    [SerializeField] private Text _columnText;
    [SerializeField] private Text _mineText;

    [SerializeField] private Slider _linesSlider;
    [SerializeField] private Slider _columnsSlider;
    [SerializeField] private Slider _minesSlider;

    public GameMode gameMode;
    private GameDataMode currentGameData;

    [SerializeField] private Text _gridText;
    [SerializeField] private Text _bombText;

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        _linesSlider.onValueChanged.AddListener(OnLinesSliderValueChanged);
        _columnsSlider.onValueChanged.AddListener(OnColumnsSliderValueChanged);
        _minesSlider.onValueChanged.AddListener(OnMinesSliderValueChanged);

        currentGameData = GameManager.Instance.GetGameDataModeFromGameMode(gameMode);
        UpdateSliderValueFromGameData();
        UpdateMaxMinesValue();
    }

    private void Update()
    {
        //UpdateCoinReward();
    }

    public void UpdateSliderValueFromGameData()
    {
        _linesSlider.value = currentGameData.Height;
        _columnsSlider.value = currentGameData.Width;
        _minesSlider.value = currentGameData.Mine;

        UpdateCoinReward();
    }

    private void OnLinesSliderValueChanged(float newValue)
    {
        currentGameData.Height = (int)newValue;
        _lineText.text = newValue.ToString();
        UpdateMaxMinesValue();
    }

    private void OnColumnsSliderValueChanged(float newValue)
    {
        currentGameData.Width = (int)newValue;
        _columnText.text = newValue.ToString();
        UpdateMaxMinesValue();
    }


    private void OnMinesSliderValueChanged(float newValue)
    {
        int maxMines = Mathf.FloorToInt(currentGameData.Height * currentGameData.Width * 0.35f);
        int clampedValue = Mathf.Clamp(Mathf.FloorToInt(newValue), 10, maxMines);
        currentGameData.Mine = clampedValue;
        _mineText.text = clampedValue.ToString();
    }

    private void UpdateMaxMinesValue()
    {
        int maxMines = Mathf.FloorToInt(currentGameData.Height * currentGameData.Width * 0.35f);
        _minesSlider.maxValue = maxMines;
        _minesSlider.minValue = 10;
    }

    public void OnCustomSetting()
    {
        UpdateCoinReward();
        UpdateSliderValueFromGameData();
        gameObject.SetActive(false);


        _gridText.text = currentGameData.Height.ToString() + "x" + currentGameData.Width.ToString();
        _mineText.text = currentGameData.Mine.ToString();
    }

    private void UpdateCoinReward()
    {
        int Grids = currentGameData.Height * currentGameData.Width;

        switch(Grids)
        {
            case 81:
                currentGameData.coinReward = 2;
                break;
            case var n when (n > 81 && n < 256):
                currentGameData.coinReward = 10;
                break;
            case var n when (n > 256 && n < 720):
                currentGameData.coinReward = 32;
                break;
            default:
                currentGameData.coinReward = 46;
                break;
        }
    }
}
