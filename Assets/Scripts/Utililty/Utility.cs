using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
    public static int ToIntSign(this bool val)
    {
        return val ? 1 : -1;
    }
}
