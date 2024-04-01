using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.Events;

public class BuildingBlocksCreator : MonoBehaviour
{
    [SerializeField]
    Transform Museum;

    Vector3 SpawnPosition = new Vector3(7.15f, 2, 0);

    string WorkshoplayerName = "Grabable";


    public void CreateForSpawn(BuildingBlock BuildingBlock)
    {
        GameObject gameObject = CreateObject(BuildingBlock);

        gameObject.transform.position = SpawnPosition;

        BuildingBlockObject buildingBlock = gameObject.GetComponent<BuildingBlockObject>();
        buildingBlock.SetUp(BuildingBlock.PrefabName, BuildingBlock.AlbedoPath, BuildingBlock.RoughnessPath, BuildingBlock.NormalMapPath);

        gameObject.AddComponent<OnMuseumAreaEnter>();
        OnMuseumAreaEnter onMuseumAreaEnter = gameObject.GetComponent<OnMuseumAreaEnter>();

        int layerIndex = LayerMask.NameToLayer(WorkshoplayerName);
        gameObject.layer = layerIndex;

        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        XRGrabInteractable interactible = gameObject.AddComponent<XRGrabInteractable>();
        interactible.movementType = XRBaseInteractable.MovementType.VelocityTracking;

        Adjustment adjustment= gameObject.AddComponent<Adjustment>();
        adjustment.gameObject = gameObject;

        StartCoroutine(WaitOneFrame(interactible, adjustment));

    }
    public void CreateForWorkshop(BuildingBlock BuildingBlock, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject gameObject = CreateObject(BuildingBlock);

        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.transform.localScale = scale;


        BuildingBlockObject buildingBlock = gameObject.GetComponent<BuildingBlockObject>();
        buildingBlock.SetUp(BuildingBlock.PrefabName);

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

    IEnumerator WaitOneFrame(XRGrabInteractable interactible,Adjustment adjustment)
    {
        yield return null;
        interactible.selectExited.AddListener(adjustment.Adjust);
        Debug.Log("One frame has passed.");
    }



    public void CreateForVisitor(BuildingBlock BuildingBlock, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject gameObject = CreateObject(BuildingBlock);

        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.transform.localScale = scale;

    }

private GameObject CreateObject(BuildingBlock BuildingBlock)
    {
        string BuildingBlockPrefabName = BuildingBlock.PrefabName;

        ResourceFromPath resourceFromPath = new ResourceFromPath();

        Texture2D albedo = new Texture2D(2048, 2048);
        Texture2D rough = new Texture2D(2048, 2048);
        Texture2D normal = new Texture2D(2048, 2048);

        if (!string.IsNullOrEmpty(BuildingBlock.AlbedoPath))
        {
            albedo = resourceFromPath.LoadTexture(BuildingBlock.AlbedoPath);
        }
        if (!string.IsNullOrEmpty(BuildingBlock.RoughnessPath))
        {
            rough = resourceFromPath.LoadTexture(BuildingBlock.RoughnessPath);
        }
        if (!string.IsNullOrEmpty(BuildingBlock.NormalMapPath))
        {
            normal = resourceFromPath.LoadTexture(BuildingBlock.NormalMapPath);
        }
        
        GameObject PrefabObject = Resources.Load<GameObject>("BuildingBlocks/" + BuildingBlockPrefabName);

        GameObject BuildingBlockObject = Instantiate(PrefabObject, Museum);


        Material newMaterial = new Material(Shader.Find("Standard"));
        if (albedo != null)
        {
            newMaterial.SetTexture("_MainTex", albedo);
        }

        if (rough != null)
        {
            newMaterial.SetTexture("_MetallicGlossMap", rough);
        }

        if (normal != null)
        {
            newMaterial.SetTexture("_BumpMap", normal);
        }
        Renderer renderer = BuildingBlockObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
        }

        return BuildingBlockObject;


    }

}
