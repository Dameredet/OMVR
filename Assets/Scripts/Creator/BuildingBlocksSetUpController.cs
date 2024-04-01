using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BuildingBlocksSetUpController : MonoBehaviour
{
    [SerializeField]
    GameObject WalkableArea;
    [SerializeField]
    Collider[] colliders;
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "MuseumBuilder")
        {
            WalkableArea.SetActive(false);

            foreach(Collider collider in colliders)
            collider.enabled = false;
        }
        else
        {
            WalkableArea.SetActive(true);

            foreach (Collider collider in colliders)
            collider.enabled = true;
        }
    }


}
