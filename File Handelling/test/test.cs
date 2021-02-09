using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class test : MonoBehaviour
{
    void Start()
    {
        GetPos();
    }

    void GetPos()
    {
        string path = "Assets/Scenes/test/pos.txt";
        string pos = ReadString(path);
        string[] p_ar = pos.Split(';');
        print(p_ar[0] + "|" + p_ar[1] + "|" + p_ar[2]);
        float x=  float.Parse(p_ar[0]);
        float y = float.Parse(p_ar[1]);
        float z = float.Parse(p_ar[2]);
        Vector3 newPos = new Vector3(x, y, z);
        gameObject.transform.position = newPos;
    }

    string ReadString(string path)
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string pos=reader.ReadToEnd();
        reader.Close();
        return pos;
    }
}
