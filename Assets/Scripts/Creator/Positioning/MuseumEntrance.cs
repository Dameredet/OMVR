using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class MuseumEntrance : MonoBehaviour
{
    public MuseumCreationDataManager museumDataManager;
    public Collider museumAreaCollider;
    [SerializeField]
    private GameObject gameObject;
    [SerializeField]
    private GameObject ExitCanvas;
    [SerializeField]
    public GameObject StartingPosition;

    [SerializeField]
    public Collider StartingPositionCollider;
    void Awake()
    {

        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "MuseumBuilder")
        {
            GameObject museumAreaObject = GameObject.Find("MuseumArea");

            if (museumAreaObject != null)
            {
                museumAreaCollider = museumAreaObject.GetComponent<Collider>();
                if (museumAreaCollider == null)
                {
                    Debug.LogError("Collider component not found on MuseumArea object.");
                }
            }
            else
            {
                Debug.LogError("MuseumArea object not found in the scene.");
            }


            GameObject museumDataManagerObject = GameObject.Find("WorkshopManager");
            museumDataManager = museumDataManagerObject.GetComponent<MuseumCreationDataManager>();


            ExitCanvas.SetActive(false);
            StartingPosition.SetActive(false);
            StartingPositionCollider.enabled = false;
        }
        else
        {
            ExitCanvas.SetActive(true);
            StartingPosition.SetActive(true);
            StartingPositionCollider.enabled = true;
        }


    }
    private void OnTriggerEnter(Collider museumobject)
    {
        if (museumobject == museumAreaCollider)
        {

            museumDataManager.IsInTheZone= true;
            museumDataManager.EntranceCount = museumDataManager.EntranceCount + 1;
        }
    }


    private void OnTriggerExit(Collider museumobject)
    {
        if (museumobject == museumAreaCollider)
        {

            museumDataManager.EntranceCount = museumDataManager.EntranceCount - 1;
            if (museumDataManager.EntranceCount < 1) museumDataManager.IsInTheZone = false;

        }
    }
}
