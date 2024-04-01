using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionInSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject DescriptionInSelectObject;
    [SerializeField]
    private TextMeshProUGUI fileNameText;

    public string FilePath;

    public void SetUp(string filePath)
    {
        FilePath = filePath;
        fileNameText.text = filePath;
    }

    public void HandleFileSelection()
    {

        GameObject museumHeadlineManager = GameObject.Find("MuseumHeadlineManager");

        if (museumHeadlineManager != null)
        {
            MuseumHeadlineManager museumHeadlineManagerComponent = museumHeadlineManager.GetComponent<MuseumHeadlineManager>();

            if (museumHeadlineManagerComponent != null)
            {
                Debug.Log("Found it. Sending path");
                Debug.Log(FilePath);
                museumHeadlineManager.GetComponent<MuseumHeadlineManager>().SetUpDescription(DescriptionInSelectObject, FilePath);
            }

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleFileSelection();

    }
}
