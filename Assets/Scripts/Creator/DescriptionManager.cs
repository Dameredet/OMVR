using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class DescriptionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PreviousPanel;
    [SerializeField]
    private GameObject Selector;


    [SerializeField]
    private MuseumCreationDataManager museumDataManager;
    [SerializeField]
    private CreatorCreationSystem creationSystem;

    public GameObject DescriptionEntryPrefab;

    public Transform fileEntryContainer;

    private string directory;

    [SerializeField]
    private TextMeshProUGUI DescriptionName;

    private GameObject SelectedObject;
    private string SelectedPath;

    void Start()
    {
        directory = Application.persistentDataPath + museumDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Descriptions";
    }
    public void OpenSelector()
    {
        PreviousPanel.SetActive(false);
        Selector.SetActive(true);

        RefreshFileList();
    }
    public void CloseButton()
    {
        PreviousPanel.SetActive(true);
        Selector.SetActive(false);
    }

    public void DeleteButton()
    {
        DeleteFile();
        SetUp(null, "");
    }
    public void SelectButton()
    {
        ChangeDescription(SelectedPath);
        CloseButton();
        creationSystem.Description.description.DescriptionPath = SelectedPath;
    }
    private void DeleteFile()
    {
        Destroy(SelectedObject);
        File.Delete(SelectedPath);
    }

    private void ChangeDescription(string filePath)
    {
        DescriptionName.text = filePath;
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

            GameObject fileEntry = Instantiate(DescriptionEntryPrefab, fileEntryContainer);
            fileEntry.GetComponent<CreatorDescriptionInSelect>().SetUp(file, file);

        }
    }

    public void SetUp(GameObject gameObject, string Path)
    {
        SelectedObject = gameObject;
        SelectedPath = Path;

    }
}
