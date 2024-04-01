using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class WorkshopUIManager : MonoBehaviour
{
    public MuseumCreationDataManager MuseumCreationDataManager;

    [SerializeField]
    private GameObject MainPanel;
    [SerializeField]
    private GameObject RestartPanel;
    [SerializeField]
    private GameObject ReturnPanel;
    [SerializeField]
    private GameObject SavePanel;
    [SerializeField]
    private GameObject WarningPanel;

    [SerializeField]
    private TextMeshProUGUI WarningText;

    private void Start()
    {
        ShowMainPanel();
    }

    public void Return()
    {
        ShowReturnPanel();
    } 

    public void ReturnYes()
    {
        if (MuseumCreationDataManager.CanSave())
        {
            MuseumCreationDataManager.Save();
        }
        else
        {
            MuseumCreationDataManager.DeleteFiles();
        }

        MuseumCreationDataManager.LoadSystem.LoadScene("MainMenu", "MuseumBuilder");
    }

    public void ReturnNo()
    {
        ShowMainPanel();
    }
    public void Restart()
    {
        ShowRestartPanel();
    }

    public void RestartYes()
    {
        MuseumCreationDataManager.Restart();
    }

    public void RestartNo()
    {
        ShowMainPanel();
    }

    public void Visit()
    {
        string path = MuseumCreationDataManager.museumHeadline.HeadlinePath;
        MuseumCreationDataManager.LoadSystem.LoadScene("MuseumVisitor", "MuseumBuilder", path);
    }

    public void Save()
    {

        if (MuseumCreationDataManager.CanSave())
        {
            MuseumCreationDataManager.Save();
            ShowSavePanel();
        }
        else
        {
            ShowWarning();
            ShowSaveWarningPanel();
        }
        
    }

    private void ShowWarning()
    {
        WarningText.text = MuseumCreationDataManager.WarningSaveMassage;
    }


    private void ShowMainPanel()
    {
        MainPanel.SetActive(true);
        RestartPanel.SetActive(false);
        ReturnPanel.SetActive(false);
        SavePanel.SetActive(false);
        WarningPanel.SetActive(false);
    }

    private void ShowRestartPanel()
    {
        MainPanel.SetActive(false);
        RestartPanel.SetActive(true);
        ReturnPanel.SetActive(false);
        SavePanel.SetActive(false);
        WarningPanel.SetActive(false);
    }

    private void ShowReturnPanel()
    {
        MainPanel.SetActive(false);
        RestartPanel.SetActive(false);
        ReturnPanel.SetActive(true);
        SavePanel.SetActive(false);
        WarningPanel.SetActive(false);
    }

    private void ShowSavePanel()
    {
        MainPanel.SetActive(false);
        RestartPanel.SetActive(false);
        ReturnPanel.SetActive(false);
        SavePanel.SetActive(true);
        WarningPanel.SetActive(false);
    }

    private void ShowSaveWarningPanel()
    {
        MainPanel.SetActive(false);
        RestartPanel.SetActive(false);
        ReturnPanel.SetActive(false);
        SavePanel.SetActive(false);
        WarningPanel.SetActive(true);
    }
}
