using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        //GameManager.IsGamePaused = true;
    }
    private void OnDisable()
    {
        //GameManager.IsGamePaused = false;
    }

    public void OnStats()
    {
        SoundManager.Instance.ToggleSound();
        gameObject.SetActive(false);
    }
}
