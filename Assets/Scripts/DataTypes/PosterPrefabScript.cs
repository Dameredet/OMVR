using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Purchasing;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class PosterPrefabScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public GameObject Poster;

    [SerializeField]
    public Image image;

    public MuseumHeadline museum = new MuseumHeadline();
    
    public void SetData(MuseumHeadline _museum)
    {
        museum = _museum;

        if(museum.PosterPath != "") {
        ResourceFromPath resourceFromPath = new ResourceFromPath();
        image.sprite = resourceFromPath.SpriteFromPath(museum.PosterPath); 
        }
    }
    public void HandleFileSelection()
    {
        GameObject posterManagerObject = GameObject.Find("MainMenuUIManager");

        if (posterManagerObject != null)
        {

            if (posterManagerObject.GetComponent<MainMenuUIManager>() != null)
            {

                posterManagerObject.GetComponent<MainMenuUIManager>().SetCurrentPoster(Poster, museum);
            }

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleFileSelection();
    }
}
