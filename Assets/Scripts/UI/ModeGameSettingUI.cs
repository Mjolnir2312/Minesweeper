using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class ModeGameSettingUI : MonoBehaviour
{
    public Image[] modeImages;
    public static GameMode selectedMode { get; private set; }

    private void Awake()
    {
        UpdateSelectedButton();
    }
    private void OnEnable()
    {
        GameManager.Instance.DisableGrid();
     
    }

    private void Update()//enable bat 
    {
        //UpdateSelectedButton();
        //selectedMode = GameManager.Instance.GetSelectMode();//
    }
    public void OnEasyButtonClick()
    {
        SoundManager.Instance.ToggleSound();
        selectedMode = GameMode.Easy;
        UpdateSelectedButton();      
    }

    public void OnMediumButtonClick()
    {
        SoundManager.Instance.ToggleSound();
        selectedMode = GameMode.Medium;
        UpdateSelectedButton();
      
    }

    public void OnHardButtonClick()
    {
        SoundManager.Instance.ToggleSound();
        selectedMode = GameMode.Hard;
        UpdateSelectedButton();
    
    }

    public void OnExtremeButtonClick()
    {
        SoundManager.Instance.ToggleSound();
        selectedMode = GameMode.Extreme;
        UpdateSelectedButton();
     
    }

    public void OnCustomButtonClick()
    {
        //SoundManager.Instance.ToggleSound();
        selectedMode = GameMode.Custom;
        UpdateSelectedButton();
    
    }

    public void OnStartButtonClick()
    {
        //SoundManager.Instance.ToggleSound();
        GameManager.Instance.StartGame(selectedMode);
        gameObject.SetActive(false);
    }

    public void OutButton()
    {
        GameManager.Instance.EnableGrid();
        SoundManager.Instance.ToggleSound();
        gameObject.SetActive(false);
    }

    private void UpdateSelectedButton()
    {
        foreach (var image in modeImages)
        {
            image.gameObject.SetActive(false);
        }

        switch (selectedMode)
        {
            case GameMode.Easy:
                modeImages[0].gameObject.SetActive(true);
                break;
            case GameMode.Medium:
                modeImages[1].gameObject.SetActive(true);
                break;
            case GameMode.Hard:
                modeImages[2].gameObject.SetActive(true);
                break;
            case GameMode.Extreme:
                modeImages[3].gameObject.SetActive(true);
                break;
            case GameMode.Custom:
                modeImages[4].gameObject.SetActive(true);
                break;
        }
    }   
}
