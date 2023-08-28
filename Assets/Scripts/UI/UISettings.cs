using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Sprite[] arraySoundSpr;
    [SerializeField] private Sprite[] arrayVibrationSpr;

    private bool isSoundOn = true;
    public static bool isVibrationOn = true;

    [SerializeField]
    private Image imgBtnSound;
    [SerializeField]
    private Image imgBtnVibration;

    private GridControl _grid;

    private void OnEnable()
    {
        Init();
        _grid.DisablePlayField();
    }

    private void OnDisable()
    {
        //_grid.EnablePlayField();
    }

    private void Awake()
    {
        _grid = GameObject.Find("GridManager").GetComponent<GridControl>();
    }

    void Init()
    {
        Player.SetSound(isSoundOn);
        imgBtnSound.sprite = arraySoundSpr[isSoundOn ? 0 : 1];


        Player.SetVibration(isVibrationOn);
        imgBtnVibration.sprite = arrayVibrationSpr[isVibrationOn ? 0 : 1];
    }
    private void Start()
    {
      
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        Player.SetSound(isSoundOn);
        SoundManager.Instance.ToggleSound();
        imgBtnSound.sprite = arraySoundSpr[isSoundOn ? 0 : 1];
    }

    public void ToggleVibration()
    {
        isVibrationOn = !isVibrationOn;
        PlayerPrefs.SetInt("Vibration", isVibrationOn ? 0 : 1);
        SoundManager.Instance.ToggleSound();
        imgBtnVibration.sprite = arrayVibrationSpr[isVibrationOn ? 0 : 1];
    }    

    public void UISettingsButton()
    {
        SoundManager.Instance.ToggleSound();
        gameObject.SetActive(false);
        _grid.EnablePlayField();
    }
}
