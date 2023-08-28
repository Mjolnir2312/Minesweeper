using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SliderCoins : MonoBehaviour
{
    public Slider SliderCoin;
    private float[] milestones = { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1 };
    public Text text1;
    public Text text2;

    private float currentTimeHeld = 0f;

    private string[] milestoneTextCoins = {
        "500",
        "3000",
        "7500",
        "20000",
        "45000",
        "99999"
    };

    private string[] milestoneTextMoney =
    {
        "23,000",
        "45,000",
        "68,000",
        "114,000",
        "161,000",
        "233,000"
    };

    private void Start()
    {
        SliderCoin.onValueChanged.AddListener(SliderValueChanged);
    }

    private void SliderValueChanged(float value)
    {
        if (value < 0.1f)
        {
            SnapToMilestones(0);
        }
        else
        {
            SnapToMilestones(value);
        }
    }

    private void SnapToMilestones(float value)
    {
        int milestoneIndex = Mathf.FloorToInt(value * (milestones.Length - 1));
        float snappedValue = milestones[milestoneIndex];
        SliderCoin.value = snappedValue;
        SliderCoin.onValueChanged.AddListener(UpdateTextCoin);
        SliderCoin.onValueChanged.AddListener(UpdateTextMoney);
    }

    private void UpdateTextCoin(float value)
    {
        int milestoneIndex = Mathf.FloorToInt(value * (milestoneTextCoins.Length - 1));
        text1.text = milestoneTextCoins[milestoneIndex];
    }

    private void UpdateTextMoney(float value)
    {
        int milestoneIndex = Mathf.FloorToInt(value * (milestoneTextMoney.Length - 1));
        text2.text = milestoneTextMoney[milestoneIndex];
    }

    public void DebugLoggg()
    {
        currentTimeHeld += Time.deltaTime;

        if (currentTimeHeld >= 0.5)
        {
            Debug.Log("keep");
            currentTimeHeld = 0f; 
        }
        Debug.Log(currentTimeHeld);
    }
}
