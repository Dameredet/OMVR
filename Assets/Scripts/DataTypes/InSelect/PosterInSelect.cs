using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PosterInSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject PosterInSelectObject;

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
        Debug.Log("I WAS CLICKED> I POSTER WAS CLISCKE");
         GameObject posterManagerObject = GameObject.Find("PosterManager");

         if (posterManagerObject != null)
         {

             if (posterManagerObject.GetComponent<PosterManager>() != null)
             {
                 Debug.Log("Found it. Sending path");
                 Debug.Log(FilePath);
                 posterManagerObject.GetComponent<PosterManager>().SetUpPoster(PosterInSelectObject, FilePath);
             }

         }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleFileSelection();

    }
}
