using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlockObject : ContentObject
{
    public BuildingBlock buildingBlock;
    public void SetUp(string _PrefabName, string _AlbedoPath = "", string _RoughnessPath = "", string _NormalMapPath = "")
    {
        buildingBlock.PrefabName = _PrefabName;
        buildingBlock.AlbedoPath = _AlbedoPath;
        buildingBlock.RoughnessPath = _RoughnessPath;
        buildingBlock.NormalMapPath = _NormalMapPath;
    }
}
