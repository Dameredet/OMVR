using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SculptureManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PreviousPanel;
    [SerializeField]
    private GameObject SculptureSelector;
    [SerializeField]
    private GameObject TextureSelector;

    [SerializeField]
    private MuseumCreationDataManager museumDataManager;
    [SerializeField]
    private CreatorCreationSystem creationSystem;

    public GameObject SculptureEntryPrefab;
    public GameObject TextureEntryPrefab;

    public Transform SculpturefileEntryContainer;
    public Transform TexturefileEntryContainer;

    private string Modeldirectory;
    private string Texturedirectory;

    [SerializeField]
    private TextMeshProUGUI SculptureName;
    [SerializeField]
    private Image AlbedoImage;
    [SerializeField]
    private Image RoughnessImage;
    [SerializeField]
    private Image NormalImage;

    string WhichTexture;

    private GameObject SelectedObject;
    private string SelectedPath;

    private void Start()
    {
        Modeldirectory = Application.persistentDataPath + museumDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Sculptures";
        Texturedirectory = Application.persistentDataPath + museumDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Textures";
    }

    public void OpenSculptureSelector()
    {
        PreviousPanel.SetActive(false);
        SculptureSelector.SetActive(true);

        RefreshSculptureFileList();
    }
    public void OpenTextureSelector(string texture)
    {
        WhichTexture = texture;

        PreviousPanel.SetActive(false);
        TextureSelector.SetActive(true);

        RefreshTextureFileList();
    }

    public void CloseButton()
    {
        PreviousPanel.SetActive(true);
        SculptureSelector.SetActive(false);
        TextureSelector.SetActive(false);
    }

    public void DeleteButton()
    {
        DeleteFile();
        SetUp(null, "");
    }
    private void DeleteFile()
    {
        Destroy(SelectedObject);
        System.IO.File.Delete(SelectedPath);
    }
    public void SelectTextureButton()
    {
        ChangeTexture(SelectedPath);
        CloseButton();
        SendTexture();
       
    }
    private void ChangeTexture(string filePath)
    {
        ResourceFromPath resourceFromPath = new ResourceFromPath();
        if(WhichTexture == "albedo") AlbedoImage.sprite = resourceFromPath.SpriteFromPath(filePath);
        else
        {
            if (WhichTexture == "roughness") RoughnessImage.sprite = resourceFromPath.SpriteFromPath(filePath);
            else if(WhichTexture == "normal") NormalImage.sprite = resourceFromPath.SpriteFromPath(filePath);
        }
    }
    private void SendTexture()
    {
      //  if (WhichTexture == "albedo") //creationSystem.Sculpture.AlbedoPath = SelectedPath;
      //  else
     //   {
          //  if (WhichTexture == "roughness") creationSystem.Sculpture.RoughnessPath = SelectedPath;
          //  else if (WhichTexture == "normal") creationSystem.Sculpture.NormalMapPath = SelectedPath;
      //  }
         
    }

    public void SelectSculptureButton()
    {
        ChangeSculpture(SelectedPath);
        CloseButton();
      //  creationSystem.Sculpture.ModelPath = SelectedPath;
    }
    private void ChangeSculpture(string filePath)
    {
        SculptureName.text = filePath;
    }

    private void RefreshSculptureFileList()
    {
        foreach (Transform child in SculpturefileEntryContainer)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("Directory:   " + Modeldirectory);
        string[] files = Directory.GetFiles(Modeldirectory);

        foreach (string file in files)
        {

            GameObject fileEntry = Instantiate(SculptureEntryPrefab, SculpturefileEntryContainer);
            fileEntry.GetComponent<ModelInSelect>().SetUp(file, file);

        }
    }
    private void RefreshTextureFileList()
    {
        foreach (Transform child in TexturefileEntryContainer)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("Directory:   " + Texturedirectory);
        string[] files = Directory.GetFiles(Texturedirectory);

        foreach (string file in files)
        {

            GameObject fileEntry = Instantiate(TextureEntryPrefab, TexturefileEntryContainer);
            fileEntry.GetComponent<TextureInSelect>().SetUp(file);

        }
    }

    public void SetUp(GameObject gameObject, string Path)
    {
        SelectedObject = gameObject;
        SelectedPath = Path;

    }
}
