using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUIManager : MonoBehaviour
{
    private string MuseumHeadlinesPath = "/Headlines";

    public IDataHandler JSONDataHandler = new JsonDataHandler();

    public MuseumLoadSystem MuseumLoadSystem;

    [SerializeField]
    private GameObject PopUp;

    [SerializeField]
    private GameObject RightPanel;

    [SerializeField]
    private ScrollRect scrollView;

    public List<GameObject> Posters = new List<GameObject>();

    [SerializeField]
    private GameObject PosterPrefab;

    [SerializeField]
    private RectTransform PostersCarusel;

    [SerializeField]
    private TextMeshProUGUI Name;

    [SerializeField]
    private TextMeshProUGUI Curator;

    [SerializeField]
    private TextMeshProUGUI Description;

    public GameObject CurrentPoster;

    void Start()
    {
        PlayerPrefs.SetString("FileToLoad", "");
        PlayerPrefs.Save();

        HidePanel();
        CreatePosters();


        DisactivateRightPanel();
        
    }

    private void ActivateRightPanel()
    {
        Debug.Log("HI");
        RightPanel.SetActive(true);
    }
    private void DisactivateRightPanel()
    {
        RightPanel.SetActive(false);
    }
  

    public void SetCurrentPoster(GameObject poster, MuseumHeadline museum)
    {
        ActivateRightPanel();
        CurrentPoster = poster;
        UpdatePosterData(museum);
    }

    private void UpdatePosterData(MuseumHeadline museum)
    {
        Debug.Log("I've been called.");
        Debug.Log(museum.Name);
        if (museum.Name != null) Name.text = museum.Name;
        else Name.text = " ";
        if (museum.Curator != null) Curator.text = museum.Curator;
        else Curator.text = " ";
        if (museum.Description != null) Description.text = museum.Description;
        else Curator.text = " ";
    }
    void CreatePosters()
    {
        List<MuseumHeadline> availableMuseumsList = GetAvailableMuseumsFromFiles();
        Debug.Log("Lista muzeum liczba   " + availableMuseumsList.Count);

        foreach (MuseumHeadline museum in availableMuseumsList)
        {
            GameObject contentInstance = Instantiate(PosterPrefab, PostersCarusel);
            if (contentInstance.TryGetComponent<PosterPrefabScript>(out PosterPrefabScript poster))
            {
                poster.SetData(museum);
            }

            Posters.Add(contentInstance);

        }
    }

    List<MuseumHeadline> GetAvailableMuseumsFromFiles()
    {
        List<MuseumHeadline> availableMuseumsList = new List<MuseumHeadline>();
        List<string> availableMuseumHeadlines = JSONDataHandler.FindJsonFilesInDirectory(MuseumHeadlinesPath);

        foreach (string museum in availableMuseumHeadlines)
        {
            availableMuseumsList.Add(JSONDataHandler.LoadData<MuseumHeadline>(museum));
        }
        return availableMuseumsList;
    }


    private void HidePanel()
    {
        PopUp.SetActive(false);
        RightPanel.SetActive(true);
    }

    private void ShowPanel()
    {
        PopUp.SetActive(true);
        RightPanel.SetActive(false);
    }

    public void EnterMuseum()
    {
        string path = CurrentPoster.GetComponent<PosterPrefabScript>().museum.HeadlinePath;
        MuseumLoadSystem.LoadScene("MuseumVisitor", "MainMenu", path);
    }

    public void EditMuseum()
    {
        string path = CurrentPoster.GetComponent<PosterPrefabScript>().museum.HeadlinePath;
        MuseumLoadSystem.LoadScene("MuseumBuilder", "MainMenu", path);
    }

    public void DeleteMuseumButton()
    {
        ShowPanel();
    }

    public void ConfirmDeleteButton()
    {
        DeleteMuseum();
    }

    public void CancelDeleteButton()
    {
        HidePanel();
    }

    private void DeleteMuseum()
    {
        Posters.Remove(CurrentPoster);
        Directory.Delete(Application.persistentDataPath + CurrentPoster.GetComponent<PosterPrefabScript>().museum.MuseumContentsDirectoryPath, true);
        Destroy(CurrentPoster);
        SaveAvailableMuseumsList();
        HidePanel();
        DisactivateRightPanel();
    }

    public void CreateMuseum()
    {
        MuseumLoadSystem.LoadScene("MuseumBuilder", "MainMenu");
    }

    private void SaveAvailableMuseumsList()
    {
        JSONDataHandler.DeleteAllFilesInDirectory(MuseumHeadlinesPath);
        foreach(GameObject poster in Posters)
        {
            poster.TryGetComponent<PosterPrefabScript>(out PosterPrefabScript p);
            JSONDataHandler.SaveData<MuseumHeadline>(MuseumHeadlinesPath + p.museum.HeadlinePath, p.museum);
        }
        MuseumLoadSystem.LoadScene("MainMenu", "MainMenu");
    }


}
