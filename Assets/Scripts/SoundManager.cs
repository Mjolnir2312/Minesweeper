using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource _buttonSource, _tickSource, _winSource, _loseSource, _mineSource;
    [SerializeField] private AudioClip _buttonClip, _tickClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TickSound()
    {
        _tickSource.PlayOneShot(_tickClip);
    }

    
    public void ToggleSound()
    {
        _buttonSource.PlayOneShot(_buttonClip);
    }

    public void WinSound()
    {
        _winSource.Play();
    }    

    public void LoseSound()
    {
        _loseSource.Play();
    }

    public void MineSound()
    {
        _mineSource.Play();
    }

    public void TurnSound()
    {
        _tickSource.mute = !_tickSource.mute;
        _buttonSource.mute = !_buttonSource.mute;
        _loseSource.mute = !_loseSource.mute;
        _winSource.mute = !_winSource.mute;
        _mineSource.mute = !_mineSource.mute;
    }

}
