using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreatorCreationSystem : MonoBehaviour
{
    [SerializeField]
    private Transform Museum;

    [SerializeField]
    public PaintingObject Painting;

    [SerializeField]
    public DescriptionObject Description;

    [SerializeField]
    public PaintingCreator createPaintingManager;

    [SerializeField]
    public DescriptionCreator createDescriptionManager;

    [SerializeField]
    private TextMeshProUGUI SculpturePathText;
    private string sculpturepathtext = "Path to sculpture file";
    [SerializeField]
    private TextMeshProUGUI DescriptionPathText;
    private string descriptionpathtext = "Path to sculpture file";
    [SerializeField]
    private Image AlbedoImage;
    [SerializeField]
    private Image RoughnessImage;
    [SerializeField]
    private Image NormalImage;
    [SerializeField]
    private Image PaintingImage;

    public string CreateWarningMessage;


    private ICreationStrategy creationStrategy;

    public void SetStrategy(string type)
    {
        if (type == "painting")
        {
            creationStrategy = new PaintingStrategy();
        }
        if (type == "sculpture")
        {
            //creationStrategy = new SculptureStrategy();
        }
        if (type == "description")
        {
            creationStrategy = new DescriptionStrategy();
        }
    }

    public void SetPaintingFrame(string FrameName)
    {
        Painting.painting.FrameName = FrameName;
    }

    public void SetInfoTab(string TabName)
    {
        Description.description.InfoTabName= TabName;
    }
    public void CleanCreationFields()
    {

        Painting.painting = new Painting();
        Description.description = new Description();

        AlbedoImage.sprite = null;
        RoughnessImage.sprite = null;
        NormalImage.sprite = null; 
        PaintingImage.sprite = null;

        SculpturePathText.text = sculpturepathtext;
        DescriptionPathText.text = descriptionpathtext;
    }

    public bool CanCreate()
    {
        bool can = creationStrategy.CanCreate(Painting.painting, Description.description);
        CreateWarningMessage = creationStrategy.GetWarningMessage();
        return can;
    }

    public void Create()
    {
        creationStrategy.Create(Painting.painting, Description.description,  createPaintingManager,  createDescriptionManager);

    }

    private interface ICreationStrategy
    {
        string GetWarningMessage();
        bool CanCreate(Painting Painting,  Description description);

        void Create(Painting Painting,  Description description, PaintingCreator createPaintingManager, DescriptionCreator createDescriptionManager);

    }

    private class PaintingStrategy : ICreationStrategy
    {
        string message;

        public string GetWarningMessage()
        {
            return message;
        }

        public bool CanCreate(Painting Painting,  Description description)
        {
            message = "Can't create because: ";

            if (string.IsNullOrEmpty(Painting.PaintingPath))
            {
                message = message + "\n - No painting has been selected";
            }
            if (string.IsNullOrEmpty(Painting.FrameName))
            {
                message = message + "\n - No frame has been selected";
            }

            if (message == "Can't create because: ")
            {
                message = "Created!";
                return true;
            }

            return false;
        }

        public void Create(Painting Painting, Description description, PaintingCreator createPaintingManager,  DescriptionCreator createDescriptionManager)
        {

            createPaintingManager.CreateForSpawn(Painting);
        }

    }


    private class DescriptionStrategy : ICreationStrategy
    {
        string message;

        public string GetWarningMessage()
        {
            return message;
        }

        public bool CanCreate(Painting Painting, Description description)
        {
            message = "Can't create because: ";

            if (string.IsNullOrEmpty(description.DescriptionPath))
            {
                message = message + "\n - No d has been selected";
            }
            if (string.IsNullOrEmpty(description.InfoTabName))
            {
                message = message + "\n - No info tab has been selected";
            }

            if (message == "Can't create because: ")
            {
                message = "Created!";
                return true;
            }

            return false;
        }

        public void Create(Painting Painting, Description description, PaintingCreator createPaintingManager, DescriptionCreator createDescriptionManager)
        {
            
            createDescriptionManager.CreateForSpawn(description);
        }

    }
}
            


  
