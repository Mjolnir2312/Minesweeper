using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingPopup;
    [SerializeField] private float displayDuration = 7f;
    [SerializeField] private Image cooldown;
    [SerializeField] private Text cooldownText;

    private void Start()
    {
        StartCoroutine(DisplayLoadingPopup());
    }

    private IEnumerator DisplayLoadingPopup()
    {
        loadingPopup.SetActive(true);
        GameManager.Instance.DisableTile();

       
        float fillIncrement = Time.deltaTime / displayDuration;

       
        cooldown.fillAmount = 0f;
        cooldownText.text = "0%";

        while (cooldown.fillAmount < 1f)
        {
            cooldown.fillAmount += fillIncrement;
            int percent = Mathf.RoundToInt(cooldown.fillAmount * 100);
            cooldownText.text = percent + "%";
            yield return null; 
        }

        GameManager.Instance.EnableTile();
        loadingPopup.SetActive(false);
    }
}




