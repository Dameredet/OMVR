using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sculpture : Content
{
    public string ModelPath;
    public string AlbedoPath;
    public string RoughnessPath;
    public string NormalMapPath;

    public void SetUp(string _ModelPath,string _AlbedoPath,string _RoughnessPath,string _NormalMapPath)
    {
        ModelPath= _ModelPath;
        AlbedoPath= _AlbedoPath;
        RoughnessPath= _RoughnessPath;
        NormalMapPath= _NormalMapPath;
    }
}
