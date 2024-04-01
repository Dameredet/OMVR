using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileInImporter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI fileNameText;
    [SerializeField]
    private Image thumbnailImage;

    private string FilePath;

    public void SetUp(string name,  string filePath)
    {
        fileNameText.text = name;
        FilePath = filePath;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.GetComponentInParent<DataImporter>().SelectFile(FilePath);
    }
}
