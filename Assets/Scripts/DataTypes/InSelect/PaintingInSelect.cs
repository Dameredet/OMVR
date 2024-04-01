using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PaintingInSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject PaintingInSelectObject;

    [SerializeField]
    private Image thumbnailImage;

    public string FilePath;

    public void SetUp(string filePath)
    {

        FilePath = filePath;
        ResourceFromPath resourceFromPath = new ResourceFromPath();
        thumbnailImage.sprite = resourceFromPath.SpriteFromPath(filePath);
        AdjustImageSize(thumbnailImage.sprite);

    }

    private void AdjustImageSize(Sprite sprite)
    {
        float spriteWidth = sprite.texture.width;
        float spriteHeight = sprite.texture.height;

        float spriteAspectRatio = spriteWidth / spriteHeight;

        RectTransform rectTransform = thumbnailImage.GetComponent<RectTransform>();

        float newWidth = rectTransform.rect.height * spriteAspectRatio;
        float newHeight = rectTransform.rect.width / spriteAspectRatio;


        if (newWidth <= rectTransform.rect.width)
        {
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.rect.height);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, newHeight);
        }
    }

    public void HandleFileSelection()
    {

        GameObject CreatorManager = GameObject.Find("CreatorManager");

        if (CreatorManager != null)
        {

            if (CreatorManager.GetComponent<PaintingManager>() != null)
            {
                Debug.Log("Found it. Sending path");
                Debug.Log(FilePath);
                CreatorManager.GetComponent<PaintingManager>().SetUpPainting(PaintingInSelectObject, FilePath);
            }

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleFileSelection();

    }
}

