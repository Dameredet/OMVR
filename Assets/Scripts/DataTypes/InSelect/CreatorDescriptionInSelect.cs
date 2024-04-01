using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreatorDescriptionInSelect : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject gameObject;

    [SerializeField]
    private TextMeshProUGUI fileNameText;


    private string FilePath;

    public void SetUp(string name, string filePath)
    {
        fileNameText.text = name;
        FilePath = filePath;
    }
    public void HandleFileSelection()
    {
        GameObject CreatorManager = GameObject.Find("CreatorManager");

        if (CreatorManager != null)
        {

            if (CreatorManager.GetComponent<DescriptionManager>() != null)
            {
                Debug.Log("Found it. Sending path");
                Debug.Log(FilePath);
                CreatorManager.GetComponent<DescriptionManager>().SetUp(gameObject, FilePath);
            }

        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        HandleFileSelection();
    }
}