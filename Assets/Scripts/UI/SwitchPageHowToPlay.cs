using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPageHowToPlay : MonoBehaviour
{
    public GameObject[] Pages;
    int index;

    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _nextButton;

    private void Start()
    {
        index = 0;
        UpdatePage();
    }



    public void Next()
    {
        index += 1;

        if (index >= Pages.Length)
        {
            index = Pages.Length - 1;
            _nextButton.image.color = Color.gray;
        }

        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].gameObject.SetActive(false);
            Pages[index].gameObject.SetActive(true);
            _nextButton.image.color = Color.white;
        }
        SoundManager.Instance.ToggleSound();
        UpdatePage();
    }

    public void Previous()
    {
        index -= 1;

        if(index < 0)
            index = 0;

        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].gameObject.SetActive(false);
            Pages[index].gameObject.SetActive(true);
            _previousButton.image.color = Color.white;
        }
        SoundManager.Instance.ToggleSound();
        UpdatePage();
    }

    private void UpdatePage()
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].SetActive(i == index);
        }

        if (index == 0)
        {
            _previousButton.image.color = Color.gray;
        }
        else
        {
            _previousButton.image.color = Color.white;
        }

        if (index == Pages.Length - 1)
        {
            _nextButton.image.color = Color.gray;
        }
        else
        {
            _nextButton.image.color = Color.white;
        }
    }
}
