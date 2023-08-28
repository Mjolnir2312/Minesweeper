using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGameUI : MonoBehaviour
{
    [SerializeField] private GameObject continueGameUI;
    [SerializeField] private GameObject loseGameUI;
    [SerializeField] private ParticleSystem countSystem;

    [SerializeField] private Text countTime;
    [SerializeField] private Text coinsContiune;
    [SerializeField] private Text coinsGame;
    [SerializeField] private float displayDuration = 15f;

    private float currentTime;
    //private GridManager gridManager;

    public CoinsData coins;

    private Coroutine showLoseCoroutine;

    private void OnEnable()
    {
        currentTime = displayDuration;
        showLoseCoroutine = StartCoroutine(ShowLoseGame());
        coinsGame.text = Player.ShowCoin().ToString();
    }
    private void Start()
    {
        //gridManager = GetComponent<GridManager>();
        coinsContiune.text = coins.CoinContinue.ToString();
    }

    private IEnumerator ShowLoseGame()
    {
        continueGameUI.SetActive(true);
        while(currentTime > 0)
        {
            countTime.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
        }

        countTime.text = "";

        if (continueGameUI.activeSelf)
        {
            continueGameUI.SetActive(false);
            loseGameUI.SetActive(true);
            GameManager.Instance.GameLost();
        }

        if (countSystem != null)
        {
            countSystem.Stop();
        }
    }

    public void TurnOff()
    {
        continueGameUI.SetActive(false);
        if (showLoseCoroutine != null)
        {
            StopCoroutine(showLoseCoroutine);
            loseGameUI.SetActive(true);
            GameManager.Instance.GameLost();
        }
        SoundManager.Instance.ToggleSound();

    }

    public void ContinueWithCoin()
    {
        SoundManager.Instance.ToggleSound();
        float coinHas = Player.ShowCoin();
        if(coinHas < coins.CoinContinue)
        {
            return;
        }
        else
        {
            Player.AddOrRemoveCoin(-coins.CoinContinue);
            GameManager.Instance.ContinueWhenLoseManager();
            continueGameUI.SetActive(false);
        }
    }
    public void ContinueWithVideo()
    {
        GameManager.Instance.ContinueWhenLoseManager();
        SoundManager.Instance.ToggleSound();
        continueGameUI.SetActive(false);
    }
}
