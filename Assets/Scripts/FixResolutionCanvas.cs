using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DP.Tools
{
    public enum ScreenType
    {
        None,
        Doc9x16,
        Doc10x16,
        Doc9x18,
        Doc9x19,
        Doc2x3,
        Doc3x4,
        //Ngang16x9,
        //Ngang16x10,
        //Ngang18x9,
        //Ngang19x9,
        //Ngang3x2,
        //Ngang4x3,
    };

    public class FixResolutionCanvas : MonoBehaviour
    {
        public static FixResolutionCanvas current;

        //============================================================================================================================================
        
        //============================================================================================================================================
        
       
        public Camera MainCam;

       
        public bool Doc;

        
        //public bool Ngang;

        //============================================================================================================================================
        
        public string screenRatio;

        public ScreenType screenType = 0;

        //============================================================================================================================================

        //#region [MAN HINH NGANG]

        ////============================================================================================================================================
        //// Màn hình tỉ lệ 4:3
        //public float WitdhOrHeightFactor43;

        //// Màn hình tỉ lệ 3:2
        //public float WitdhOrHeightFactor32;

        //// Màn hình tỉ lệ 16:10
        //public float WitdhOrHeightFactor1610;

        ////============================================================================================================================================
        ////============================================================================================================================================
        //// Màn hình tỉ lệ 16:9
        //public float WitdhOrHeightFactor169;

        //// Màn hình tỉ lệ 18:9
        //public float WitdhOrHeightFactor189;

        //// Màn hình tỉ lệ 19:9
        //public float WitdhOrHeightFactor199;

        ////============================================================================================================================================

        //#endregion

        #region [MAN HINH DOC]

        //============================================================================================================================================
        // Màn hình tỉ lệ 3:4
        public float WitdhOrHeightFactor34;

        // Màn hình tỉ lệ 2:3
        public float WitdhOrHeightFactor23;

        // Màn hình tỉ lệ 10:16
        public float WitdhOrHeightFactor1016;

        //============================================================================================================================================
        //============================================================================================================================================
        // Màn hình tỉ lệ 9:16
        public float WitdhOrHeightFactor916;

        // Màn hình tỉ lệ 9:18
        public float WitdhOrHeightFactor918;

        // Màn hình tỉ lệ 9:19
        public float WitdhOrHeightFactor919;

        //============================================================================================================================================

        #endregion

        //============================================================================================================================================

      
        private bool isChangeSpecialObj;

       
        private RectTransform rectTextPlay;

        //============================================================================================================================================
        void Awake()
        {
            current = this;
            SetWindowAspectRatio();
        }

        private void Start()
        {
            CheckAndLogScreenRatio();
        }

        private void Update()
        {
        }

        //============================================================================================================================================
        void SetWindowAspectRatio()
        {
            float _screenRatio = MainCam.aspect;
            string _screenRatio_ = _screenRatio.ToString("F2");
            screenRatio = _screenRatio_.Substring(0, 4);
            //Debug.Log("=================> Ratio= " + screenRatio);
            //-------------------------------------------------------------------
            if (this.Doc)
                SetWindowAspectRatioDoc();
            //if (this.Ngang)
            //    SetWindowAspectRatioNgang();
        }

        void SetWindowAspectRatioDoc()
        {
            switch (screenRatio)
            {
                case "0.75":
                    screenType = ScreenType.Doc3x4;
                    GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor34;
                    break;
                case "0.67":
                    screenType = ScreenType.Doc2x3;
                    GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor23;
                    break;
                case "0.63":
                    screenType = ScreenType.Doc10x16;
                    GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor1016;
                    break;
                case "0.56":
                    screenType = ScreenType.Doc9x16;
                    GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor916;
                    break;
                case "0.50":
                    screenType = ScreenType.Doc9x18;
                    GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor918;
                    break;
                case "0.46":
                case "0.47":
                case "0.49":
                    screenType = ScreenType.Doc9x19;
                    GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor919;
                    break;
                default:
                    break;
            }

            var _ratio = float.Parse(screenRatio);
            if (_ratio <= 0.5f)
            {
                screenType = ScreenType.Doc9x18;
                GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor918;
            }

            OnChangeSpecialObj();
        }

        void CheckAndLogScreenRatio()
        {
            float currenAspectRatio = MainCam.aspect;

            if( currenAspectRatio <= 0.75 &&  currenAspectRatio >= 0.67)
            {
                MainCam.orthographicSize = 3;
            }    
            else if(currenAspectRatio <= 0.63 && currenAspectRatio >= 0.56)
            {
                MainCam.orthographicSize = 4;
            }
            else if(currenAspectRatio <= 0.5 && currenAspectRatio >= 0.4)
            {
                MainCam.orthographicSize = 5;
            }
            else
                MainCam.orthographicSize = 6.1f;
        }

        //void SetWindowAspectRatioNgang()
        //{
        //    switch (screenRatio)
        //    {
        //        case "0.75":
        //            screenType = ScreenType.Ngang4x3;
        //            GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor43;
        //            break;
        //        case "0.67":
        //            screenType = ScreenType.Ngang3x2;
        //            GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor32;
        //            break;
        //        case "0.63":
        //            screenType = ScreenType.Ngang16x10;
        //            GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor1610;
        //            break;
        //        case "0.56":
        //            screenType = ScreenType.Ngang16x9;
        //            GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor169;
        //            break;
        //        case "0.50":
        //            screenType = ScreenType.Ngang18x9;
        //            GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor189;
        //            break;
        //        case "0.46":
        //        case "0.47":
        //        case "0.49":
        //            screenType = ScreenType.Ngang19x9;
        //            GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor199;
        //            break;
        //        default:
        //            break;
        //    }

        //    var _ratio = float.Parse(screenRatio);
        //    if (_ratio <= 0.5f)
        //    {
        //        screenType = ScreenType.Ngang18x9;
        //        GetComponent<CanvasScaler>().matchWidthOrHeight = WitdhOrHeightFactor189;
        //    }

        //    OnChangeSpecialObj();
        //}

        void OnChangeSpecialObj()
        {
            if (!isChangeSpecialObj) return;
           // rectTextPlay.anchoredPosition = 
        }

        //============================================================================================================================================
        public virtual bool CheckLongDisplay()
        {
            float _screenRatio = MainCam.aspect;
            string _screenRatio_ = _screenRatio.ToString("F2");
            screenRatio = _screenRatio_.Substring(0, 4);
            var _ratio = float.Parse(screenRatio);
            var _rs = _ratio <= 0.5f ? true : false;
            return _rs;
        }
    }
}