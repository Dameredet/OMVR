using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSelector : MonoBehaviour
{
    [SerializeField]
    private CreatorCreationSystem creationSystem;
    public void SelectNoFrame()
    {
        creationSystem.SetPaintingFrame("NoFrame");
    }

    public void SelectFrame()
    {
        creationSystem.SetPaintingFrame("Frame");
    }
}
