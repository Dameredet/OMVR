using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTabSelector : MonoBehaviour
{
    [SerializeField]
    private CreatorCreationSystem creationSystem;
    public void SelectHanging()
    {
        creationSystem.SetInfoTab("Hanging");
    }

    public void SelectStanding()
    {
        creationSystem.SetInfoTab("Standing");
    }
}
