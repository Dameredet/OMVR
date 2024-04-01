using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatorUILogic : MonoBehaviour
{

    [SerializeField]
    private CreatorCreationSystem creatorCreationSystem;


    [SerializeField]
    private GameObject ModeSelect;
    [SerializeField]
    private GameObject BuildingBlocksPanel;
    [SerializeField]
    private GameObject PaintingPanel;
    [SerializeField]
    private GameObject SculpturePanel;
    [SerializeField]
    private GameObject DescriptionPanel;

    [SerializeField]
    private GameObject PaintingSelectPanel;
    [SerializeField]
    private GameObject FrameSelectPanel;

    [SerializeField]
    private GameObject BuildingBlocksSelectPanel;
    [SerializeField]
    private GameObject TexturesSelectPanel;

    [SerializeField]
    private GameObject DescriptionSelectPanel;
    [SerializeField]
    private GameObject DisplaySelectPanel;

    [SerializeField]
    private GameObject CreateWarningPanel;
    [SerializeField]
    private TextMeshProUGUI WarningText;

    [SerializeField]
    private GameObject Selector;

    private GameObject CurrentPanel;

    public void ReturnButton()
    {
        creatorCreationSystem.CleanCreationFields();
        SwitchModeView(ModeSelect);
    }

    public void BuildingBlocksSelectButton()
    {
        SwitchModeView(BuildingBlocksPanel);
    }
    public void PaintingSelectButton() 
    {
        SwitchModeView(PaintingPanel);
        creatorCreationSystem.SetStrategy("painting");
    }
    public void SculptureSelectButton()
    {
        SwitchModeView(SculpturePanel);
        creatorCreationSystem.SetStrategy("sculpture");
    }
    public void DescriptionSelectButton()
    {
        SwitchModeView(DescriptionPanel);
        creatorCreationSystem.SetStrategy("description");
    }


    public void PaintingStepButton()
    {
        SwitchPaitingPanelViews(PaintingSelectPanel);
    }
    public void FrameStepButton()
    {
        SwitchPaitingPanelViews(FrameSelectPanel);
    }
    public void BuildingBlocksStepButton()
    {
        SwitchSculpturePanelViews(BuildingBlocksSelectPanel);
    }
    public void TexturesStepButton()
    {
        SwitchSculpturePanelViews(TexturesSelectPanel);
    }

    public void DescriptionStepButton()
    {
        SwitchDescriptionPanelViews(DescriptionSelectPanel);
    }
    public void InfoTabStepButton()
    {
        SwitchDescriptionPanelViews(DisplaySelectPanel);
    }

    public void CreateButton()
    {
        if (creatorCreationSystem.CanCreate())
        {
            creatorCreationSystem.Create();
        }

        ShowCreateWarning(true);
    }
    public void OKButton()
    {
        ShowCreateWarning(false);
    }
    private void SwitchModeView(GameObject view)
    {
        if (BuildingBlocksPanel != null && BuildingBlocksPanel != view)
            BuildingBlocksPanel.SetActive(false);
        if (ModeSelect != null && ModeSelect != view)
            ModeSelect.SetActive(false);
        if (PaintingPanel != null && PaintingPanel != view)
            PaintingPanel.SetActive(false);
        if (SculpturePanel != null && SculpturePanel != view)
            SculpturePanel.SetActive(false);
        if (DescriptionPanel != null && DescriptionPanel != view)
            DescriptionPanel.SetActive(false);

        if (view != null)
            view.SetActive(true);

        CurrentPanel = view;
    } 

    private void SwitchPaitingPanelViews(GameObject view)
    {
        if (PaintingSelectPanel != null && PaintingSelectPanel != view)
            PaintingSelectPanel.SetActive(false);
        if (FrameSelectPanel != null && FrameSelectPanel != view)
            FrameSelectPanel.SetActive(false);

        if (view != null)
            view.SetActive(true);
    }

    private void SwitchSculpturePanelViews(GameObject view)
    {
        if (BuildingBlocksSelectPanel != null && BuildingBlocksSelectPanel != view)
            BuildingBlocksSelectPanel.SetActive(false);
        if (TexturesSelectPanel != null && TexturesSelectPanel != view)
            TexturesSelectPanel.SetActive(false);

        if (view != null)
            view.SetActive(true);
    }

    private void SwitchDescriptionPanelViews(GameObject view)
    {
        if (DescriptionSelectPanel != null && DescriptionSelectPanel != view)
            DescriptionSelectPanel.SetActive(false);
        if (DisplaySelectPanel != null && DisplaySelectPanel != view)
            DisplaySelectPanel.SetActive(false);

        if (view != null)
            view.SetActive(true);
    }

    private void ShowCreateWarning(bool activate)
    {
        CurrentPanel.GetComponent<CanvasGroup>().interactable = !activate;

        CreateWarningPanel.SetActive(activate);

        WarningText.text = creatorCreationSystem.CreateWarningMessage; 
    }


}
