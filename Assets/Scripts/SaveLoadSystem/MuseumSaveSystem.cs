using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumSaveSystem : MonoBehaviour
{
    public IDataHandler JSONDataHandler = new JsonDataHandler();
    private string MuseumHeadlinesPath = "/Headlines";

    public void SaveBothFiles(MuseumHeadline museumHeadline, MuseumContents museumContents)
    {
            SaveMuseumHeadline(museumHeadline);
            SaveMuseumContents(museumHeadline, museumContents);
    }
    public void SaveMuseumHeadline(MuseumHeadline museumHeadline)
    {
        JSONDataHandler.SaveData<MuseumHeadline>(MuseumHeadlinesPath + museumHeadline.HeadlinePath, museumHeadline);
    }

    public void SaveMuseumContents(MuseumHeadline museumHeadline, MuseumContents museumContents)
    {
        JSONDataHandler.SaveData<MuseumContents>(museumHeadline.MuseumContentsPath, museumContents);
    }

}
