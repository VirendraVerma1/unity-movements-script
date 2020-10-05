using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public static class JsonLoader
{
    public static JsonData LoadFile(string path)
    {
        var fileContents = File.ReadAllText(path);
        var data = JsonMapper.ToObject(fileContents);
        return data;
    }
}
