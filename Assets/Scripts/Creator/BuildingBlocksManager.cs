using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBlocksManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PreviousPanel;
    [SerializeField]
    private GameObject BuildingBlockSelector;
    [SerializeField]
    private GameObject TextureSelector;

    [SerializeField]
    private MuseumCreationDataManager museumDataManager;

    [SerializeField]
    BuildingBlocksCreator createBuildingBlockManager;
    [SerializeField]
    BuildingBlockObject BuildingBlock;


    public GameObject TextureEntryPrefab;
    public Transform TexturefileEntryContainer;

    private string Texturedirectory;
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
        Texturedirectory = Application.persistentDataPath + museumDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Textures";
    }

    public void SelectWall()
    {
        BuildingBlock.buildingBlock.PrefabName = "WallPrefab";

    }

    public void SelectFloor()
    {
        BuildingBlock.buildingBlock.PrefabName = "FloorPrefab";

    }

    public void SelectStairs()
    {
        BuildingBlock.buildingBlock.PrefabName = "StairsPrefab";


    }

    public void SelectEntrance()
    {
        BuildingBlock.buildingBlock.PrefabName = "EntrancePrefab";


    }

    public void OpenTextureSelector(string texture)
    {
        WhichTexture = texture;
        Debug.Log(WhichTexture);

        PreviousPanel.SetActive(false);
        TextureSelector.SetActive(true);

        RefreshTextureFileList();
    }

    public void CloseButton()
    {
        PreviousPanel.SetActive(true);
        TextureSelector.SetActive(false);
    }

    public void DeleteButton()
    {
        DeleteFile();
        ClearSelection(null, "");
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
        SelectTexture();

    }
    private void ChangeTexture(string filePath)
    {
        ResourceFromPath resourceFromPath = new ResourceFromPath();
        if (WhichTexture == "albedo") AlbedoImage.sprite = resourceFromPath.SpriteFromPath(filePath);
        else
        {
            if (WhichTexture == "roughness") RoughnessImage.sprite = resourceFromPath.SpriteFromPath(filePath);
            else if (WhichTexture == "normal") NormalImage.sprite = resourceFromPath.SpriteFromPath(filePath);
        }
    }
    private void SelectTexture()
    {
          if (WhichTexture == "albedo") BuildingBlock.buildingBlock.AlbedoPath = SelectedPath;
         else
          {
          if (WhichTexture == "roughness") BuildingBlock.buildingBlock.RoughnessPath = SelectedPath;
          else if (WhichTexture == "normal") BuildingBlock.buildingBlock.NormalMapPath = SelectedPath;
          }

    }

    public void SetUp(GameObject gameObject, string Path)
    {
        SelectedObject = gameObject;
        SelectedPath = Path;

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

    public void ClearSelection(GameObject gameObject, string Path)
    {
        SelectedObject = gameObject;
        SelectedPath = Path;

    }

    public void Create()
    {
        
        createBuildingBlockManager.CreateForSpawn(BuildingBlock.buildingBlock);
    }
}
