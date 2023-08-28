using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlaySettingsUI : MonoBehaviour
{
    [SerializeField] private Sprite[] arrZoomSpr;
    [SerializeField] private Button zoomButton;
    [SerializeField] private Image imgZoom;
    [SerializeField] private Text tapHold;
    [SerializeField] private Text autoOpenAre;
    [SerializeField] private Text holdToFlag;
    [SerializeField] private Text zooming;
    [SerializeField] private Text dragBoard;
    [SerializeField] private Text showFrame;

    [SerializeField] private GameObject frameUI;

    public static int currentChoice = 1;
    public static bool isAutoOpen = false;
    public static bool isHolding = true;
    public static bool isDragging = true;
    public static bool isShowing = true;
    public static bool isZooming = GamePlaySettings.isZoom;

    private void OnEnable()
    {
        imgZoom.sprite = arrZoomSpr[isZooming ? 0 : 1];
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Is Holding: " + isHolding);
    }

    public void ChangeFuntionTapHold()
    {
        switch (currentChoice)
        {
            case 1:
                tapHold.text = "Tap: Show menu\r\nHold: No action";
                break;
            case 2:
                tapHold.text = "Tap: New game\r\nHold: Show menu";
                break;
            case 3:
                tapHold.text = "Tap: Show menu\r\nHold: New game";
                break;
            default:
                break;
        }
    }

    public void ChangeFuntionAutoOpen()
    {
        if (isAutoOpen)
        {
            autoOpenAre.text = "YES";
        }
        else
        {
            autoOpenAre.text = "NO";
        }
        Debug.Log(isAutoOpen);
    }

    public void ChangeFuntionHoldToFlag()
    {
        if (isHolding)
        {
            holdToFlag.text = "YES";
        }
        else
        {
            holdToFlag.text = "NO";
        }
    }
    public void ChangeFuntionZooming()
    {
        if (isZooming)
        {
            zooming.text = "YES";
        }
        else
        {
            zooming.text = "NO";
        }
    }
    public void ChangeFuntionDragging()
    {
        if (isDragging)
        {
            dragBoard.text = "YES";
        }
        else
        {
            dragBoard.text = "NO";
        }
    }
    public void ChangeFuntionShowFrame()
    {
        if (isShowing)
        {
            showFrame.text = "YES";
        }
        else
        {
            showFrame.text = "NO";
        }
    }

    public void NextChoice()
    {
        currentChoice = (currentChoice % 3) + 1;
        SoundManager.Instance.ToggleSound();
        ChangeFuntionTapHold();
    }

    public void AutoOpenArea()
    {
        isAutoOpen = !isAutoOpen;
        SoundManager.Instance.ToggleSound();
        ChangeFuntionAutoOpen();
    }

    public void HoldToFlag()
    {
        isHolding = !isHolding;
        SoundManager.Instance.ToggleSound();
        ChangeFuntionHoldToFlag();
    }

    public void Zooming()
    {
        isZooming = !isZooming;
        imgZoom.sprite = arrZoomSpr[isZooming ? 0 : 1];
        SoundManager.Instance.ToggleSound();
        ChangeFuntionZooming();
    }

    public void DragBoard()
    {
        isDragging = !isDragging;
        SoundManager.Instance.ToggleSound();
        ChangeFuntionDragging();
    }

    public void ShowFrame()
    {
        isShowing = !isShowing;
        SoundManager.Instance.ToggleSound();
        ChangeFuntionShowFrame();

        if (isShowing)
        {
            frameUI.SetActive(true);
        }
        else
            frameUI.SetActive(false);
    }
}
