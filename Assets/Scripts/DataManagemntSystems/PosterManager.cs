using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PosterManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PosterPanel;
    [SerializeField]
    private GameObject SelectPanel;

    [SerializeField]
    private MuseumCreationDataManager museumDataManager;

    public GameObject fileEntryPrefab;
    public Transform fileEntryContainer;

    private string directory;

    [SerializeField]
    private Image PosterImage;

    private GameObject SelectedPoster;

    [SerializeField]
    private string SelectedPosterPath;


    void Start()
    {
        directory = Application.persistentDataPath + museumDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Poster";
        ShowPosterPanel();
    }

    public MuseumHeadline UIToData(MuseumHeadline museumHeadline)
    {
        museumHeadline.PosterPath = SelectedPosterPath;


        return museumHeadline;
    }

    public void DataToUI(MuseumHeadline museumHeadline)
    {
        SelectedPosterPath = museumHeadline.PosterPath;
        ChangePoster(museumHeadline.PosterPath);

    }

    public void CloseButton()
    {
        ShowPosterPanel();
    }
    public void PosterButton()
    {
        ShowSelectPanel();
        Debug.Log("Show");
        RefreshFileList();
    }
    public void DeleteButton()
    {
        DeleteFile();
        SetUpPoster(null, "");
    }

    public void SelectButton()
    {
        ChangePoster(SelectedPosterPath);
        ShowPosterPanel();
    }

    private void ShowPosterPanel()
    {
        SelectPanel.SetActive(false);
        PosterPanel.SetActive(true);
    } 
    private void ShowSelectPanel()
    {
        SelectPanel.SetActive(true);
        PosterPanel.SetActive(false);
    }

    private void DeleteFile()
    {
        Destroy(SelectedPoster);
        File.Delete(SelectedPosterPath);
    }

    private void ChangePoster(string filePath)
    {
        ResourceFromPath resourceFromPath = new ResourceFromPath();
        PosterImage.sprite = resourceFromPath.SpriteFromPath(filePath);
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
                fileEntry.GetComponent<PosterInSelect>().SetUp(file);

        }
    }

    public void SetUpPoster(GameObject gameObject, string Path)
    {
        SelectedPoster = gameObject;
        SelectedPosterPath = Path;

        Debug.Log("SetUp Selected Poster and the path is: " + Path);
    }
}
