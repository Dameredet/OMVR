using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Adjustment : MonoBehaviour
{
    public GameObject gameObject;
    private AdjustmentUI adjustmentUI;
    void Start()
    {
        GameObject gm = GameObject.Find("MuseumBuildBlocksAdjustmentUI");
        adjustmentUI = gm.GetComponent<AdjustmentUI>();
    }

    public void Adjust(SelectExitEventArgs arg0)
    {
        adjustmentUI.SetMuseumObject(gameObject);
    }
}
