using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextureInSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject TextureInSelectObject;

    [SerializeField]
    private Image thumbnailImage;

    public string FilePath;

    public void SetUp(string filePath)
    {

        FilePath = filePath;
        ResourceFromPath resourceFromPath = new ResourceFromPath();
        thumbnailImage.sprite = resourceFromPath.SpriteFromPath(filePath);


    }

    public void HandleFileSelection()
    {

        GameObject CreatorManager = GameObject.Find("CreatorManager");

        if (CreatorManager != null)
        {

            if (CreatorManager.GetComponent<BuildingBlocksManager>() != null)
            {
                Debug.Log("Found it. Sending path");
                Debug.Log(FilePath);
                CreatorManager.GetComponent<BuildingBlocksManager>().SetUp(TextureInSelectObject, FilePath);
            }

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleFileSelection();

    }
}
