using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGameUI : MonoBehaviour
{
    GameMode SelectedMode;

    [SerializeField] private Text record;
    [SerializeField] private Text bombFound;
    [SerializeField] private Text winReward;
    [SerializeField] private Text coinReward;

    private void OnEnable()
    {
        SelectedMode = ModeGameSettingUI.selectedMode;
        //Debug.Log(SelectedMode.ToString());

        GameDataMode gameDataMode = GameManager.Instance.GetGameDataModeFromGameMode(SelectedMode);

        int sum = gameDataMode.Mine + gameDataMode.coinReward;

        float timeRecord = GameManager.Instance.TimeRecord();
        Debug.Log(SelectedMode);
        //Player.BestTime(timeRecord, SelectedMode);
        //Player.ShowTime(timeRecord, SelectedMode);
        Debug.Log(Player.ShowTime(timeRecord, SelectedMode));
        record.text = Math.Round(Player.ShowTime(timeRecord, SelectedMode), 3).ToString("0.000");

        bombFound.text = gameDataMode.Mine.ToString();
        winReward.text = gameDataMode.coinReward.ToString();
        coinReward.text = sum.ToString();

        Player.AddOrRemoveCoin(sum);
    }
    private void Awake()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OutWinGameUIButton()
    {
        gameObject.SetActive(false);
        SoundManager.Instance.ToggleSound();
    }

    public void ResetGame()
    {
        GameManager.Instance.Reset();
        gameObject.SetActive(false);
        SoundManager.Instance.ToggleSound();
    }
}
