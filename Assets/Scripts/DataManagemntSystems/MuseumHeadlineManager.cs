using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;

public class MuseumHeadlineManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField museumName;
    [SerializeField]
    private TMP_InputField museumCurator;
    [SerializeField]
    private TMP_InputField museumDescription;

    [SerializeField]
    private GameObject DataPanel;
    [SerializeField]
    private GameObject SelectPanel;

    [SerializeField]
    private MuseumCreationDataManager museumDataManager;

    public GameObject fileEntryPrefab;
    public Transform fileEntryContainer;
    private string directory;


    private GameObject SelectedDescription;
    private string SelectedDescriptionPath;
    void Start()
    {
        directory = Application.persistentDataPath + museumDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Descriptions";
        ShowDataPanel();
    }
    public MuseumHeadline UIToData(MuseumHeadline museumHeadline) 
    { 
        museumHeadline.Name = museumName.text;
        museumHeadline.Curator =  museumCurator.text;
        museumHeadline.Description = museumDescription.text;

        return museumHeadline;
    }

    public void DataToUI(MuseumHeadline museumHeadline)
    {
        museumName.text = museumHeadline.Name;
        museumCurator.text = museumHeadline.Curator;
        museumDescription.text = museumHeadline.Description;

    }
    public void CloseButton()
    {
        ShowDataPanel();
    }
    public void DescriptionButton()
    {
        ShowSelectPanel();
        RefreshFileList();
    }
    public void DeleteButton()
    {
        DeleteFile();
        SetUpDescription(null, "");
    }

    public void SelectButton()
    {
        ChangeDescription(SelectedDescriptionPath);
        ShowDataPanel();
    }
    private void ShowDataPanel()
    {
        SelectPanel.SetActive(false);
        DataPanel.SetActive(true);
    }
    private void ShowSelectPanel()
    {
        SelectPanel.SetActive(true);
        DataPanel.SetActive(false);
    }

    private void DeleteFile()
    {
        Destroy(SelectedDescription);
        File.Delete(SelectedDescriptionPath);
    }

    private void ChangeDescription(string filePath)
    {
        string fileContents = File.ReadAllText(filePath);
        museumDescription.text = fileContents;
    }
    private void RefreshFileList()
    {
        foreach (Transform child in fileEntryContainer)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("Directory:   " + directory);
        string[] files = Directory.GetFiles(directory);

        foreach (string file in files)
        {

            GameObject fileEntry = Instantiate(fileEntryPrefab, fileEntryContainer);
            fileEntry.GetComponent<DescriptionInSelect>().SetUp(file);

        }
    }

    public void SetUpDescription(GameObject gameObject, string Path)
    {
        SelectedDescription = gameObject;
        SelectedDescriptionPath = Path;

    }
}
