using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool _toggleMusic;
    [SerializeField] private GameObject _offSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMusicButton(bool On)
    {
        _offSound.SetActive(On);
        Debug.Log(On);
    }

    //public void ToggleMusic()
    //{
    //    if (_toggleMusic) SoundManager.Instance.ToggleSound();
    //}
}
