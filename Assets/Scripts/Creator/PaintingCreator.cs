using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class PaintingCreator : MonoBehaviour
{
    [SerializeField]
    Transform Museum;

    Vector3 SpawnPosition = new Vector3(7.15f, 2, 0);

    public float WorkshopScale;

    int pictureWidth;
    int pictureHeight;

    string WorkshoplayerName = "Grabable";

    public void CreateForSpawn(Painting Painting)
    {
        GameObject gameObject = CreateObject(Painting);

        //float aspectRatio = (float)pictureWidth / (float)pictureHeight;

        Vector3 currentScale = gameObject.transform.localScale;

        //Vector3 newScale = new Vector3(currentScale.x * WorkshopScale, currentScale.y * WorkshopScale, currentScale.z * WorkshopScale);
        //gameObject.transform.localScale = newScale;

        gameObject.transform.position = SpawnPosition;

        PaintingObject painting = gameObject.GetComponent<PaintingObject>();
        painting.SetUp(Painting.PaintingPath, Painting.FrameName);

        gameObject.AddComponent<OnMuseumAreaEnter>();

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
    public void CreateForWorkshop(Painting Painting, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject gameObject = CreateObject(Painting);

        gameObject.transform.position = position;
        gameObject.transform.rotation= rotation;
        gameObject.transform.localScale = scale;

        PaintingObject painting = gameObject.GetComponent<PaintingObject>();
        painting.SetUp(Painting.PaintingPath, Painting.FrameName);

        gameObject.AddComponent<OnMuseumAreaEnter>();

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

    public void CreateForVisitor(Painting Painting, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject gameObject = CreateObject(Painting);


        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.transform.localScale = scale;

    }
    private GameObject CreateObject(Painting Painting)
    {
        string picturePath = Painting.PaintingPath;
        string framePrefabName = Painting.FrameName;
        ResourceFromPath resourceFromPath = new ResourceFromPath();
        Texture2D texture = resourceFromPath.LoadTexture(picturePath);

        if (texture != null)
        {
            pictureWidth = texture.height;
            pictureHeight = texture.width;


            GameObject canvasPrefab = Resources.Load<GameObject>("Frames/Canvas");

            if (canvasPrefab != null)
            {
                GameObject canvasObject = Instantiate(canvasPrefab, Museum);

                Renderer canvasRenderer = canvasObject.GetComponent<Renderer>();
                if (canvasRenderer != null)
                {
                    canvasRenderer.material.mainTexture = texture;

                    float aspectRatio = (float)pictureWidth / (float)pictureHeight;

                    Vector3 canvasScale = new Vector3(aspectRatio*20f, 20f, 20f);
                    canvasObject.transform.localScale = canvasScale;

                   // canvasObject.transform.Rotate(Vector3.up, 180f);
                   //   canvasObject.transform.Rotate(180f, 0f, 0f);
                  //  canvasObject.transform.Rotate(Vector3.forward, 180f);

                    GameObject framePrefab = Resources.Load<GameObject>("Frames/" + framePrefabName);

                    if (framePrefab != null)
                    {
                        GameObject frameObject = Instantiate(framePrefab, canvasObject.transform);
                        frameObject.transform.Rotate(Vector3.forward, 180f);
                        frameObject.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        Debug.LogError("Frame prefab not found in Resources folder: " + framePrefabName);
                    }

                    return canvasObject; 
                }
                else
                {
                    Debug.LogError("Renderer component not found on Canvas object");
                }
            }
            else
            {
                Debug.LogError("Canvas prefab not found in Resources/Frames folder");
            }
        }
        else
        {
            Debug.LogError("Failed to load picture texture at path: " + picturePath);
        }

        return null; 
    }

}
