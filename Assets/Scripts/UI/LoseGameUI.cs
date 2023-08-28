using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text bombFounds;
    [SerializeField] private Text coinRewards;

    private void OnEnable()
    {
        int bombFound = GameManager.Instance.MineFound();

        bombFounds.text = bombFound.ToString();
        coinRewards.text = bombFound.ToString();

        Player.AddOrRemoveCoin(bombFound);
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

    public void ResetGame()
    {
        GameManager.Instance.Reset();
        gameObject.SetActive(false);
        SoundManager.Instance.ToggleSound();
    }
}
