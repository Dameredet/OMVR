using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMuseumAreaEnter : MonoBehaviour
{
    public Collider museumAreaCollider;
    public Collider DeleteArea;

    public MuseumContentsManager contentsManager;
    private void Start()
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

        GameObject DeleteAreaObject = GameObject.Find("DeleteArea");

        if (DeleteAreaObject != null)
        {
            DeleteArea = DeleteAreaObject.GetComponent<Collider>();
            if (DeleteArea == null)
            {
                Debug.LogError("Collider component not found on DeleteArea object.");
            }
        }
        else
        {
            Debug.LogError("DeleteArea object not found in the scene.");
        }

        contentsManager = FindObjectOfType<MuseumContentsManager>();
    }
    private void OnTriggerEnter(Collider museumobject)
    {
        if (museumobject == museumAreaCollider)
        {
            if (gameObject.GetComponent<PaintingObject>() != null)
            {
                contentsManager.paintingObjects.Add(gameObject.GetComponent<PaintingObject>());
            }
            else if (gameObject.GetComponent<DescriptionObject>() != null)
            {
                contentsManager.descriptionObjects.Add(gameObject.GetComponent<DescriptionObject>());
            }
            else if (gameObject.GetComponent<BuildingBlockObject>() != null)
            {
                contentsManager.buildingBlockObjects.Add(gameObject.GetComponent<BuildingBlockObject>());
            }
        }

        if (museumobject == DeleteArea)
        {
            if (gameObject.GetComponent<PaintingObject>() != null)
            {
                contentsManager.paintingObjects.Remove(gameObject.GetComponent<PaintingObject>());
            }
            else if (gameObject.GetComponent<DescriptionObject>() != null)
            {
                contentsManager.descriptionObjects.Remove(gameObject.GetComponent<DescriptionObject>());
            }
            else if (gameObject.GetComponent<BuildingBlockObject>() != null)
            {
                contentsManager.buildingBlockObjects.Remove(gameObject.GetComponent<BuildingBlockObject>());
            }

            Destroy(gameObject);
        }
    }


    private void OnTriggerExit(Collider museumobject)
    {
        if (museumobject == museumAreaCollider)
        {
            if (gameObject.GetComponent<PaintingObject>() != null)
            {
                contentsManager.paintingObjects.Remove(gameObject.GetComponent<PaintingObject>());
            }
            else if (gameObject.GetComponent<DescriptionObject>() != null)
            {
                contentsManager.descriptionObjects.Remove(gameObject.GetComponent<DescriptionObject>());
            }
            else if (gameObject.GetComponent<BuildingBlockObject>() != null)
            {
                contentsManager.buildingBlockObjects.Remove(gameObject.GetComponent<BuildingBlockObject>());
            }
        }
    }


}
