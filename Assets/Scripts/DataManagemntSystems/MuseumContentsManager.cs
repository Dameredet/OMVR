using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MuseumContentsManager : MonoBehaviour
{

    [SerializeField]
    public PaintingCreator createPaintingManager;

    [SerializeField]
    public DescriptionCreator createDescriptionManager;

    [SerializeField]
    BuildingBlocksCreator createBuildingBlockManager;

    public List<PaintingObject> paintingObjects = new List<PaintingObject>();
    public List<DescriptionObject> descriptionObjects = new List<DescriptionObject>();
    public List<BuildingBlockObject> buildingBlockObjects = new List<BuildingBlockObject>();

    public MuseumContents UIToData(MuseumContents museumContents)
    {
        museumContents.paintings.Clear();
        museumContents.descriptions.Clear();
        museumContents.buildingblocks.Clear();
        foreach (PaintingObject painting in paintingObjects)
        {
            painting.SetTransform(painting.painting, 
                painting.gameObject.transform);
            museumContents.paintings.Add
                (painting.painting);
        }
        foreach (DescriptionObject description in descriptionObjects)
        {
            description.SetTransform(description.description, description.gameObject.transform);
            museumContents.descriptions.Add(description.description);

        }
        foreach (BuildingBlockObject buildingBlock in buildingBlockObjects)
        {
            buildingBlock.SetTransform(buildingBlock.buildingBlock, buildingBlock.gameObject.transform);
            museumContents.buildingblocks.Add(buildingBlock.buildingBlock);
        }

        return museumContents;
    }

    public void DataToUI(MuseumContents museumContents)
    {
        DestroyObjects();

        foreach (Painting painting in museumContents.paintings) 
        {
            PaintingObject paintingObject = new PaintingObject();
            paintingObject.painting = painting;
            createPaintingManager.CreateForWorkshop(painting, paintingObject.GetPosition(painting), paintingObject.GetRotation(painting),paintingObject.GetScale(painting));
            paintingObjects.Add(paintingObject);
        }
        foreach (Description description in museumContents.descriptions)
        {
            DescriptionObject descriptionObject = new DescriptionObject();
            descriptionObject.description = description;
            createDescriptionManager.CreateForWorkshop(description, descriptionObject.GetPosition(description), descriptionObject.GetRotation(description), descriptionObject.GetScale(description));
            descriptionObjects.Add(descriptionObject);
        }
        foreach (BuildingBlock buildingBlock in museumContents.buildingblocks)
        {
            BuildingBlockObject buildingBlockObject = new BuildingBlockObject();
            buildingBlockObject.buildingBlock = buildingBlock;
            createBuildingBlockManager.CreateForWorkshop(buildingBlock, buildingBlockObject.GetPosition(buildingBlock), buildingBlockObject.GetRotation(buildingBlock), buildingBlockObject.GetScale(buildingBlock));
            buildingBlockObjects.Add(buildingBlockObject);
        }
        museumContents = new MuseumContents();
    }

   private void DestroyObjects()
    {
        foreach (PaintingObject painting in paintingObjects)
        {
            
            Destroy(painting.gameObject);
            paintingObjects.Remove(painting);
        }
        foreach (DescriptionObject description in descriptionObjects)
        {
            
            Destroy(description.gameObject);
            descriptionObjects.Remove(description);

        }
        foreach (BuildingBlockObject buildingBlock in buildingBlockObjects)
        {
            
            Destroy(buildingBlock.gameObject);
            buildingBlockObjects.Remove(buildingBlock);
        }
    }
}