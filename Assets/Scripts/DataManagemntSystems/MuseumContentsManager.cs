using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MuseumContentsManager : MonoBehaviour
{

    [SerializeField]
    PaintingCreator createPaintingManager;

    [SerializeField]
    DescriptionCreator createDescriptionManager;

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
       // DestroyObjects();
       
        foreach (Painting painting in museumContents.paintings) 
        {
            PaintingObject paintingObject = new PaintingObject();
            paintingObject.painting = painting;
            createPaintingManager.CreateForWorkshop(painting, paintingObject.GetPosition(painting), paintingObject.GetRotation(painting),paintingObject.GetScale(painting));
            //paintingObjects.Add(paintingObject);
        }
        foreach (Description description in museumContents.descriptions)
        {
            DescriptionObject descriptionObject = new DescriptionObject();
            descriptionObject.description = description;
            createDescriptionManager.CreateForWorkshop(description, descriptionObject.GetPosition(description), descriptionObject.GetRotation(description), descriptionObject.GetScale(description));
            //descriptionObjects.Add(descriptionObject);
        }
        int i = 0;
        foreach (BuildingBlock buildingBlock in museumContents.buildingblocks)
        {
            Debug.Log("This is a building block nr: " + i + " " + buildingBlock.PrefabName);
            i++;
            BuildingBlockObject buildingBlockObject = new BuildingBlockObject();
            buildingBlockObject.buildingBlock = buildingBlock;
            createBuildingBlockManager.CreateForWorkshop(buildingBlock, buildingBlockObject.GetPosition(buildingBlock), buildingBlockObject.GetRotation(buildingBlock), buildingBlockObject.GetScale(buildingBlock));
            //buildingBlockObjects.Add(buildingBlockObject);
        }
       

    }

   private void ClearObjects()
    {
        foreach (PaintingObject painting in paintingObjects)
        {
            if(painting == null)
            paintingObjects.Remove(painting);
            Destroy(painting.gameObject);
            
        }
        foreach (DescriptionObject description in descriptionObjects)
        {
            descriptionObjects.Remove(description);
            Destroy(description.gameObject);
            

        }
        foreach (BuildingBlockObject buildingBlock in buildingBlockObjects)
        {
            buildingBlockObjects.Remove(buildingBlock);
            Destroy(buildingBlock.gameObject);
            Debug.Log("I have destroyed an object");
            
        }
    }
}