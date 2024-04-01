using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingObject : ContentObject
{
    public Painting painting;

    public void SetUp(string _PaintingPath, string _FrameName)
    {
        painting.PaintingPath = _PaintingPath;
        painting.FrameName = _FrameName;
    }
}
