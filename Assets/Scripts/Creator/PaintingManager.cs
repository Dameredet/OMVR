using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PaintingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PreviousPanel;
    [SerializeField]
    private GameObject Selector;


    [SerializeField]
    private MuseumCreationDataManager museumDataManager;

    [SerializeField]
    private CreatorCreationSystem creationSystem;

    public GameObject PaintingEntryPrefab;

    public Transform fileEntryContainer;

    private string directory;

    [SerializeField]
    private Image PaintingImage;

    private GameObject SelectedObject;
    private string SelectedPath;

    void Start()
    {
        directory = Application.persistentDataPath + museumDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Paintings";
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
        SetUpPainting(null, "");
    }
    public void SelectButton()
    {
        ChangePainting(SelectedPath);
        CloseButton();
        creationSystem.Painting.painting.PaintingPath = SelectedPath;
    }
    private void DeleteFile()
    {
        Destroy(SelectedObject);
        File.Delete(SelectedPath);
    }

    private void ChangePainting(string filePath)
    {
        ResourceFromPath resourceFromPath = new ResourceFromPath();
        PaintingImage.sprite = resourceFromPath.SpriteFromPath(filePath);
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

            GameObject fileEntry = Instantiate(PaintingEntryPrefab, fileEntryContainer);
            fileEntry.GetComponent<PaintingInSelect>().SetUp(file);

        }
    }

    public void SetUpPainting(GameObject gameObject, string Path)
    {
        SelectedObject = gameObject;
        SelectedPath = Path;

    }
}
