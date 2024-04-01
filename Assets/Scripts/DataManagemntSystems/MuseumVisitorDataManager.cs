using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumVisitorDataManager : MonoBehaviour
{
    public MuseumHeadline museumHeadline = new MuseumHeadline();
    private string MuseumHeadlinesPath = "/Headlines";
    public MuseumContents museumContents = new MuseumContents();
    private string FileToLoad;

    private IDataHandler JSONDataHandler = new JsonDataHandler();

    [SerializeField]
    public PaintingCreator createPaintingManager;

    [SerializeField]
    public DescriptionCreator createDescriptionManager;

    [SerializeField]
    BuildingBlocksCreator createBuildingBlockManager;

    [SerializeField]
    VisitorSpawn spawn;
    void Start()
    {
        FileToLoad = PlayerPrefs.GetString("FileToLoad", "");
        ReadPassedData();
        CreateMuseum();
        spawn.SetSpawner();


    }
    private void ReadPassedData()
    {
        museumHeadline = JSONDataHandler.LoadData<MuseumHeadline>(MuseumHeadlinesPath + FileToLoad);
        museumContents = JSONDataHandler.LoadData<MuseumContents>(museumHeadline.MuseumContentsPath);
    }
    public void CreateMuseum()
    {

        foreach (Painting painting in museumContents.paintings)
        {
            PaintingObject paintingObject = new PaintingObject();
            paintingObject.painting = painting;
            createPaintingManager.CreateForVisitor(painting, paintingObject.GetPosition(painting), paintingObject.GetRotation(painting), paintingObject.GetScale(painting));

        }
        foreach (Description description in museumContents.descriptions)
        {
            DescriptionObject descriptionObject = new DescriptionObject();
            descriptionObject.description = description;
            createDescriptionManager.CreateForVisitor(description, descriptionObject.GetPosition(description), descriptionObject.GetRotation(description), descriptionObject.GetScale(description));

        }
        foreach (BuildingBlock buildingBlock in museumContents.buildingblocks)
        {
            BuildingBlockObject buildingBlockObject = new BuildingBlockObject();
            buildingBlockObject.buildingBlock = buildingBlock;
            createBuildingBlockManager.CreateForVisitor(buildingBlock, buildingBlockObject.GetPosition(buildingBlock), buildingBlockObject.GetRotation(buildingBlock), buildingBlockObject.GetScale(buildingBlock));

        }

    }
}
