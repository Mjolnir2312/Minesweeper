using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Text _coin;
    [SerializeField] private Text _coinAds;

    public CoinsData coinsData;
    //private GridControl _grid;
    private void OnEnable()
    {
        //GameManager.IsGamePaused = true;
        _coin.text = Player.ShowCoin().ToString();
        GameManager.Instance.DisableGrid();
    }

    private void Awake()
    {
        //_grid = GameObject.Find("GridManager").GetComponent<GridControl>();
    }

    private void Start()
    {
        _coinAds.text = "+" + coinsData.CoinAdd.ToString();
    }

    private void Update()
    {
        //_coin.text = Player.ShowCoin().ToString();
    }

    private void OnDisable()
    {
        //GameManager.IsGamePaused = false;
    }

    public void ShopUIButton()
    {
        this.gameObject.SetActive(false);
        SoundManager.Instance.ToggleSound();
        GameManager.Instance.EnableGrid();
    }

    public void AddCoinVideoAds()
    {
        Player.AddOrRemoveCoin(coinsData.CoinAdd);
        SoundManager.Instance.ToggleSound();
    }
}
