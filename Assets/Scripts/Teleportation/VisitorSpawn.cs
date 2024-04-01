using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class VisitorSpawn : MonoBehaviour
{
    [SerializeField]
    Transform origin;

    [SerializeField]
    RecenterManager RecenterManager;

    public Transform startingpoint;
    public void SetSpawner()
    {
        GetStartingPoint();

        origin.position = startingpoint.position;

        RecenterManager.target = startingpoint;
    }

    private void GetStartingPoint()
    {
        GameObject Entrance = GameObject.Find("EntrancePrefab(Clone)");
        if (Entrance != null)
        {

            MuseumEntrance scriptComponent = Entrance.GetComponent<MuseumEntrance>();

            if (scriptComponent != null)
            {
                startingpoint = scriptComponent.StartingPositionCollider.transform;
            }
            else
            {
                Debug.LogWarning("Script component not found on the GameObject!");
            }
        }
        else
        {
            Debug.LogWarning("GameObject not found!");
        }
    }

}
