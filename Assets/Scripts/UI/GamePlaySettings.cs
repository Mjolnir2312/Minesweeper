using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlaySettings : MonoBehaviour
{
    [SerializeField] private Sprite[] arrFlagSpr;
    [SerializeField] private Button flagButton;

    [SerializeField] private Sprite[] arrZoomSpr;
    [SerializeField] private Button zoomButton;

    [SerializeField] private Image imgZoom;
    [SerializeField] private Image imgFlag;

    [SerializeField] private Text mineText;
    [SerializeField] private Text timeText;

    [SerializeField] private Camera cam;
    private float camPos;

    public static bool isFlagMode = false;
    public static bool isZoom = true;
    private void OnEnable()
    {
        imgFlag.sprite = arrFlagSpr[isFlagMode ? 1 : 0];
        imgZoom.sprite = arrZoomSpr[isZoom ? 0 : 1];
        camPos = cam.orthographicSize;
    }
    void Start()
    {
        
    }

    private void Update()
    {
        imgFlag.sprite = arrFlagSpr[isFlagMode ? 1 : 0];

        if (isFlagMode)
        {
            mineText.color = Color.green;
            timeText.color = Color.green;
        }
        else
        {
            mineText.color = Color.red;
            timeText.color = Color.red;
        }

        bool isGamePlay = GameManager.IsGameOver;
        if (isGamePlay)
        {
            flagButton.interactable = false;
        }
        else
            flagButton.interactable = true;
    }

    public void ToggleFlagMode()
    {
        SoundManager.Instance.ToggleSound();
        isFlagMode = !isFlagMode;
    }

    public void ToggleZoomMode()
    {
        isZoom = !isZoom;
        //SoundManager.Instance.ToggleSound();
        imgZoom.sprite = arrZoomSpr[isZoom ? 0 : 1];
    }

    public void ResetSizeCamera()
    {
        cam.orthographicSize = camPos;
    }

    public void ResetSizeCameraButton()
    {
        ResetSizeCamera();
        SoundManager.Instance.ToggleSound();
    }

    public void HintButton()
    {
        GameManager.Instance.HintMine();
        SoundManager.Instance.ToggleSound();
    }    
}
