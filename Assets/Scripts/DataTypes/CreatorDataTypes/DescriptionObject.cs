using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionObject : ContentObject
{
    public Description description;

    public void SetUp(string _DescriptionPath, string _InfoTabName)
    {
        description.DescriptionPath = _DescriptionPath;
        description.InfoTabName = _InfoTabName;
    }
}
