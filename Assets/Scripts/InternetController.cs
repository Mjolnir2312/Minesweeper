using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InternetController
{
    public static bool IsInternet
    {
        get
        {
            var _isInternet = Application.internetReachability == NetworkReachability.NotReachable ? false : true;
            return _isInternet;
        }
    }
}
