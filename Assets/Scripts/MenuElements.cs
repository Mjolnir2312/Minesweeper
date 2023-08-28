using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]

public class MenuElements
{
    [SerializeField] private Slider _rowSlider;
    [SerializeField] private Slider _colSlider;
    [SerializeField] private Slider _minesSlider;

    //[SerializeField] private Text _timeScaleText;

    //public Text TimeScaleText
    //{
    //    get { return _timeScaleText; }
    //    set { _timeScaleText = value; }
    //}

    public Slider RowSlider
    {
        get { return _rowSlider; }
        set { _rowSlider = value; }
    }

    public Slider ColSlider
    {
        get { return _colSlider; }
        set { _colSlider = value; }
    }

    public Slider MinesSlider
    {
        get { return _minesSlider; }
        set { _minesSlider = value; }
    }
}

