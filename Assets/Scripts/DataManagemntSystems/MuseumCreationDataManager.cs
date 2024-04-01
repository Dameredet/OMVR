using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;

public class MuseumCreationDataManager : MonoBehaviour
{
    public MuseumHeadline museumHeadline = new MuseumHeadline();
    private string MuseumHeadlinesPath = "/Headlines";

    public MuseumContents museumContents = new MuseumContents();

    private string FileToLoad;

    private IDataHandler JSONDataHandler = new JsonDataHandler();

    public MuseumSaveSystem SaveSystem;
    public MuseumLoadSystem LoadSystem;

    public MuseumHeadlineManager RightPanel;
    public PosterManager CentralPanel;
    public MuseumContentsManager Table;

    public bool IsInTheZone;
    public int EntranceCount = 0;
    public string WarningSaveMassage;

    void Awake()
    {
        FileToLoad = PlayerPrefs.GetString("FileToLoad", "");

        if (string.IsNullOrEmpty(FileToLoad))
        {
           SetUpData(); 
        }
        else
        {
           ReadPassedData();
           DataToUI(museumHeadline,museumContents);
        }
    }


    private void SetUpData()
    {
        string id = Guid.NewGuid().ToString();
        museumHeadline.HeadlinePath = "/" + id + ".json";
        museumHeadline.MuseumContentsDirectoryPath = "/" + id;

        JSONDataHandler.CreateDirectory("/" + id + "/Paintings");
        JSONDataHandler.CreateDirectory("/" + id + "/Poster");
        JSONDataHandler.CreateDirectory("/" + id + "/Descriptions");
        JSONDataHandler.CreateDirectory("/" + id + "/Sculptures");
        JSONDataHandler.CreateDirectory("/" + id + "/Textures");
        JSONDataHandler.CreateDirectory("/" + id + "/Music");

        museumHeadline.MuseumContentsPath = "/" + id + "/" + "MuseumContents.json";

    }

    private void ReadPassedData()
    {
        museumHeadline = JSONDataHandler.LoadData<MuseumHeadline>(MuseumHeadlinesPath + FileToLoad);
        museumContents = JSONDataHandler.LoadData<MuseumContents>(museumHeadline.MuseumContentsPath);
    }

    public void Save()
    {
        UIToData();
        SaveSystem.SaveBothFiles(museumHeadline, museumContents);
    }

    public bool CanSave()
    {
        UIToData();

        WarningSaveMassage = "Can't save due to: ";

        if (string.IsNullOrEmpty(museumHeadline.Name)) WarningSaveMassage = WarningSaveMassage + "\n - Missing museum name";
        if (string.IsNullOrEmpty(museumHeadline.Curator)) WarningSaveMassage = WarningSaveMassage + "\n - Missing museum curator";
        if (string.IsNullOrEmpty(museumHeadline.Description)) WarningSaveMassage = WarningSaveMassage + "\n - Missing museum description";
        if (string.IsNullOrEmpty(museumHeadline.PosterPath)) WarningSaveMassage = WarningSaveMassage + "\n - Missing museum poster";
        if (EntranceCount != 1) WarningSaveMassage = WarningSaveMassage + "\n - There can only be ONE museum entrance";


        if (WarningSaveMassage != "Can't save due to: ")
        {
            return false;
        } 

        return true;
    }
    public void DataToUI(MuseumHeadline _museumHeadline, MuseumContents _museumContents)
    {
        CentralPanel.DataToUI(_museumHeadline);
        RightPanel.DataToUI(_museumHeadline);
        Table.DataToUI(_museumContents);
    }

    public void UIToData()
    {
        museumHeadline = CentralPanel.UIToData(museumHeadline);
        museumHeadline = RightPanel.UIToData(museumHeadline);
        museumContents = Table.UIToData(museumContents);
    }

    public void Restart()
    {
        museumHeadline.Name = "";
        museumHeadline.Curator = "";
        museumHeadline.Description = "";
        museumHeadline.PosterPath = null;

        museumContents.paintings.Clear();
        museumContents.descriptions.Clear();
        museumContents.buildingblocks.Clear();
        DataToUI(museumHeadline, museumContents);
    }

    public void DeleteFiles()
    { 
        Directory.Delete(Application.persistentDataPath + museumHeadline.MuseumContentsDirectoryPath, true);
        Debug.Log("Folder with museum contents has been deleated." + Application.persistentDataPath + museumHeadline.MuseumContentsDirectoryPath);
        File.Delete(Application.persistentDataPath + MuseumHeadlinesPath + museumHeadline.HeadlinePath);
        Debug.Log("File with museum headline has been deleated." + Application.persistentDataPath + MuseumHeadlinesPath + museumHeadline.HeadlinePath);
    }
}
