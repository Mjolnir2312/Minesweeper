using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemMode : MonoBehaviour
{
    [SerializeField] private Text _gridText;
    [SerializeField] private Text _mineText;

    public GameMode gameMode;

    private int height;
    private int width;
    private int mine;

    private void OnEnable()
    {
        GameDataMode gameDataMode = GameManager.Instance.GetGameDataModeFromGameMode(gameMode);
        
        height = gameDataMode.Height;
        width = gameDataMode.Width;
        mine = gameDataMode.Mine;

        _gridText.text = height.ToString() + "x" + width.ToString();
        _mineText.text = mine.ToString();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
