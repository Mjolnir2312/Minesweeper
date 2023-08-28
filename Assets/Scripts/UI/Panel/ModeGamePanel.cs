using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ModeGamePanel : MonoBehaviour
{
    public GameObject ModeGameForm;
    public GameObject CustomSettingForm;
    public GameObject GameSettingForm;
    public GameObject GamePlaySettingForm;
    public GameObject HowToPlayForm;
    public GameObject LanguagesForm;
    public GameObject ShopForm;
    public GameObject CoinForm;
    public GameObject StatsForm;
    public GameObject WinForm;
    public GameObject LoseForm;
    public GameObject ContinueForm;


    public Image GameStatus;

    private void Update()
    {        //bool ClickTile = TileScript.OnClickTile;

        //if (ClickTile)
        //{
        //    GameStatus.sprite = GameStatus.GetComponent<StateButton>().Clicked;
        //}
        //else
        //    GameStatus.sprite = GameStatus.GetComponent<StateButton>().Default;

    }
    public void OnModeGameOptionButtonClick(bool show)
    {
        SoundManager.Instance.ToggleSound();
        ModeGameForm.SetActive(show);
    }

    public void OnCustomSetting(bool show)
    {
        SoundManager.Instance.ToggleSound();
        CustomSettingForm.SetActive(show);
    }

    public void OnGameSetting(bool show)
    {
        SoundManager.Instance.ToggleSound();
        GameSettingForm.SetActive(show);
    }

    public void OnGamePlaySetting(bool show)
    {
        SoundManager.Instance.ToggleSound();
        GamePlaySettingForm.SetActive(show);
    }

    public void OnHowToPlay(bool show)
    {
        SoundManager.Instance.ToggleSound();
        HowToPlayForm.SetActive(show);
    }

    public void OnLanguagesSetting(bool show)
    {
        SoundManager.Instance.ToggleSound();
        LanguagesForm.SetActive(show);
    }

    public void OnShopForm(bool show)
    {
        SoundManager.Instance.ToggleSound();
        ShopForm.SetActive(show);
    }

    public void OnCoinForm(bool show)
    {
        SoundManager.Instance.ToggleSound();
        CoinForm.SetActive(show);
    }

    public void OnStatsForm(bool show)
    {
        SoundManager.Instance.ToggleSound();
        StatsForm.SetActive(show);
    }

    public void OnWinForm(bool show)
    {
        SoundManager.Instance.ToggleSound();
        WinForm.SetActive(show);
        //GameStatus.sprite = GameStatus.GetComponent<StateButton>().Win;
    }

    public void OnLostForm(bool show)
    {
        SoundManager.Instance.ToggleSound();
        LoseForm.SetActive(show);
    }

    public void OnContinueForm(bool show)
    {
        SoundManager.Instance.ToggleSound();
        ContinueForm.SetActive(show);
        //GameStatus.sprite = GameStatus.GetComponent<StateButton>().Lose;
    }
}
