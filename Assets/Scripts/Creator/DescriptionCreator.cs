using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class DescriptionCreator : MonoBehaviour
{
    [SerializeField]
    Transform Museum;
    Vector3 SpawnPosition = new Vector3(7.15f, 2, 0);

    public float WorkshopScale = 0.15f;

    string WorkshoplayerName = "Grabable";
    public void CreateForSpawn(Description Description)
    {
        GameObject gameObject = CreateObjectForWorkshop(Description);

        Vector3 currentScale = gameObject.transform.localScale;

        Vector3 newScale = new Vector3(currentScale.x * WorkshopScale, currentScale.y * WorkshopScale, currentScale.z * WorkshopScale);
        gameObject.transform.localScale = newScale;

        gameObject.transform.position = SpawnPosition;

        gameObject.AddComponent<DescriptionObject>();
        DescriptionObject description = gameObject.GetComponent<DescriptionObject>();
        description.SetUp(Description.DescriptionPath, Description.InfoTabName);

        gameObject.AddComponent<OnMuseumAreaEnter>();
        OnMuseumAreaEnter onMuseumAreaEnter = gameObject.GetComponent<OnMuseumAreaEnter>();

        int layerIndex = LayerMask.NameToLayer(WorkshoplayerName);
        gameObject.layer = layerIndex;


        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        XRGrabInteractable interactible = gameObject.AddComponent<XRGrabInteractable>();
        interactible.movementType = XRBaseInteractable.MovementType.VelocityTracking;


        Adjustment adjustment = gameObject.AddComponent<Adjustment>();
        adjustment.gameObject = gameObject;
        StartCoroutine(WaitOneFrame(interactible, adjustment));
    }

    public void CreateForWorkshop(Description Description, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject gameObject = CreateObjectForWorkshop(Description);

        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.transform.localScale = scale;

        gameObject.AddComponent<DescriptionObject>();
        DescriptionObject description = gameObject.GetComponent<DescriptionObject>();
        description.SetUp(Description.DescriptionPath, Description.InfoTabName);

        gameObject.AddComponent<OnMuseumAreaEnter>();
        OnMuseumAreaEnter onMuseumAreaEnter = gameObject.GetComponent<OnMuseumAreaEnter>();

        int layerIndex = LayerMask.NameToLayer(WorkshoplayerName);
        gameObject.layer = layerIndex;

        gameObject.AddComponent<BoxCollider>();

        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        XRGrabInteractable interactible = gameObject.AddComponent<XRGrabInteractable>();
        interactible.movementType = XRBaseInteractable.MovementType.VelocityTracking;


        Adjustment adjustment = gameObject.AddComponent<Adjustment>();
        adjustment.gameObject = gameObject;
        StartCoroutine(WaitOneFrame(interactible, adjustment));

    }
    IEnumerator WaitOneFrame(XRGrabInteractable interactible, Adjustment adjustment)
    {
        yield return null;
        interactible.selectExited.AddListener(adjustment.Adjust);
        Debug.Log("One frame has passed.");
    }

    public void CreateForVisitor(Description Description, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject gameObject = CreateObjectForWorkshop(Description);

        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.transform.localScale = scale;

    }

    private GameObject CreateObjectForWorkshop(Description Description)
    {
        string descriptionPath = Description.DescriptionPath;
        string InfoTabPrefabName = Description.InfoTabName;

        GameObject infoTabPrefab = Resources.Load<GameObject>("Displays/" + InfoTabPrefabName);

        if (infoTabPrefab != null)
        {
            GameObject InfoTabObject = Instantiate(infoTabPrefab, Museum);

            TextMeshProUGUI text = InfoTabObject.GetComponentInChildren<TextMeshProUGUI>();

            if (text != null)
            {
                try
                {
                    string descriptionText = File.ReadAllText(descriptionPath);
                    text.text = descriptionText;
                }
                catch (Exception e)
                {
                    Debug.LogError("Error reading description file: " + e.Message);
                }
            }

            return InfoTabObject;
        }

        return null;

       
    }

    
}

