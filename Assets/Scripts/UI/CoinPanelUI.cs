using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPanelUI : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    public CoinsData coinsData;
    private void OnEnable()
    {
        coinsText.text = Player.ShowCoin().ToString();

        //GameManager.IsGamePaused = true;
        GameManager.Instance.DisableGrid();
    }

    private void Awake()
    {
     
    }
    private void OnDisable()
    {
        //GameManager.IsGamePaused = false;
    }

    private void Update()
    {
    }

    public void CoinUI()
    {
        GameManager.Instance.EnableGrid();
        SoundManager.Instance.ToggleSound();
        gameObject.SetActive(false);
    }
}
