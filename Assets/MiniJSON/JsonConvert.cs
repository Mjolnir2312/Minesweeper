﻿using MiniJSON;

public static class JsonConvert
{
    public static string SerializeObject(object serialize)
    {
        return Json.Serialize(serialize);
    }
    public static T DeserializeObject<T>(string serialize)
    {
        return Json.Deserialize<T>(serialize);
    }
}
