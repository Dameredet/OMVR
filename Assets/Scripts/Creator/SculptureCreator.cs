using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering;
//using UnityMeshImporter;

public class SculptureCreator : MonoBehaviour
{
    public GameObject spawnerArea;

    public float WorkshopScale = 0.2f;
    public float SaveScale = 2.0f;

    public string WorkshoplayerName = "Grabable";

    private void Start()
    {
        spawnerArea = GameObject.Find("SpawnerArea");
    }
    public void CreateForWorkshop(Sculpture Sculpture, Transform transform)
    {
        GameObject gameObject = CreateObject(Sculpture, transform);

        Vector3 currentScale = gameObject.transform.localScale;

        gameObject.transform.localScale = new Vector3(currentScale.x * WorkshopScale, currentScale.y * WorkshopScale , currentScale.z * WorkshopScale );

        //gameObject.AddComponent<Sculpture>();
        Sculpture sculpture = gameObject.GetComponent<Sculpture>();
        sculpture.SetUp(Sculpture.ModelPath, Sculpture.AlbedoPath, Sculpture.RoughnessPath, Sculpture.NormalMapPath);

        gameObject.AddComponent<OnMuseumAreaEnter>();
        OnMuseumAreaEnter onMuseumAreaEnter = gameObject.GetComponent<OnMuseumAreaEnter>();
        //onMuseumAreaEnter.gameObject = gameObject;

        int layerIndex = LayerMask.NameToLayer(WorkshoplayerName);
        gameObject.layer = layerIndex;


        gameObject.AddComponent<BoxCollider>();


        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        XRGrabInteractable interactible = gameObject.AddComponent<XRGrabInteractable>();
        interactible.movementType = XRBaseInteractable.MovementType.Kinematic;

        Adjustment adjustment = gameObject.AddComponent<Adjustment>();
        adjustment.gameObject = gameObject;

        StartCoroutine(WaitOneFrame(interactible, adjustment));


        Debug.Log("Listener Count: " + interactible.selectEntered.GetPersistentEventCount());

    }
    IEnumerator WaitOneFrame(XRGrabInteractable interactible, Adjustment adjustment)
    {
        yield return null;
        interactible.selectExited.AddListener(adjustment.Adjust);
        Debug.Log("One frame has passed.");
    }


    private GameObject CreateObject(Sculpture Sculpture, Transform transform)
    {
        ResourceFromPath resourceFromPath = new ResourceFromPath();

        string ModelPath = Sculpture.ModelPath;

      /*  GameObject gameObject = MeshImporter.Load(ModelPath);
        Instantiate(gameObject, transform.position, Quaternion.identity);

        Material material = new Material(Shader.Find("Standard"));


        Texture2D albedoTexture = resourceFromPath.LoadTexture(Sculpture.AlbedoPath);
        material.mainTexture = albedoTexture;

        if (string.IsNullOrEmpty(Sculpture.RoughnessPath))
        {
        Texture2D roughnessTexture = resourceFromPath.LoadTexture(Sculpture.RoughnessPath);
            material.SetTexture("_MetallicGlossMap", roughnessTexture);
            material.EnableKeyword("_METALLICGLOSSMAP");

        }

        if (string.IsNullOrEmpty(Sculpture.NormalMapPath))
        {
            Texture2D normalMapTexture = resourceFromPath.LoadTexture(Sculpture.NormalMapPath);
            material.SetTexture("_BumpMap", normalMapTexture);
        }
        
        Renderer renderer = gameObject.AddComponent<Renderer>();
        renderer.material = material;
      */
        return gameObject;
    }


   


}



