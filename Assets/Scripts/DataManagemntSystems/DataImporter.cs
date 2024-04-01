using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;

public class DataImporter : MonoBehaviour
{
    public TextMeshProUGUI currentFolderText;
    public GameObject fileEntryPrefab;
    public Transform fileEntryContainer;

    private IDataHandler JSONDataHandler = new JsonDataHandler();

    [SerializeField]
    private MuseumCreationDataManager museumCreationDataManager;

    [SerializeField]
    private Button importButton;
    [SerializeField]
    private GameObject PopUpPanel;
    [SerializeField]
    private GameObject ImportPanel;

    public string selectedFile;
    public string selectedDirectory;

    private string currentFolder;


    private IFileListingStrategy fileListingStrategy;

    void Start()
    {
        currentFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        selectedFile = null;
        ShowImporter();
    }

    public void PaintingButton()
    {
        fileListingStrategy = new PaintingStrategy();
        selectedDirectory = museumCreationDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Paintings";
        Refresh();
    }
    public void PosterButton() 
    {
        fileListingStrategy = new PosterStrategy();
        selectedDirectory = museumCreationDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Poster";
        Refresh();
    }
    public void DescriptionButton() 
    {
        fileListingStrategy = new DescriptionStrategy();
        Debug.Log(museumCreationDataManager.museumHeadline.MuseumContentsDirectoryPath);
        selectedDirectory = museumCreationDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Descriptions";
        Refresh();
    }
    public void SculptureButton() 
    {
        fileListingStrategy = new SculptureStrategy();
        selectedDirectory = museumCreationDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Sculptures";
        Refresh();
    }
    public void TexturesButton() 
    {
        fileListingStrategy = new PaintingStrategy();
        selectedDirectory = museumCreationDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Textures";
        Refresh();
    }
    public void MusicButton()
    {
        fileListingStrategy = new MusicStrategy();
        selectedDirectory = museumCreationDataManager.museumHeadline.MuseumContentsDirectoryPath + "/Music";
        Refresh();
    }

    private void Refresh()
    {
        RefreshFileList();
        selectedFile = null;
        importButton.interactable = false;
    }
    public void SelectFile(string FilePath)
    {
        if (Directory.Exists(FilePath))
        {
            OpenFolder(FilePath);
        }
        else
        {
            selectedFile = FilePath;
            importButton.interactable = true;
        }

    }
    public void ImportButton()
    {
        Debug.Log("SF: " + selectedFile);
        Debug.Log("SD: " + selectedDirectory);
        JSONDataHandler.CopyFileIntoAppData(selectedDirectory, selectedFile);
        ShowPopUp();
        Refresh();
    }

    public void OKButton()
    {
        ShowImporter();
    }

    private void ShowPopUp()
    {
        ImportPanel.GetComponent<CanvasGroup>().interactable = false;
        PopUpPanel.SetActive(true);
    }

    private void ShowImporter()
    {
        ImportPanel.GetComponent<CanvasGroup>().interactable = true;
        PopUpPanel.SetActive(false);
    }

    public void OpenFolder(string folderPath)
    {
        currentFolder = folderPath;
        RefreshFileList();
    }

    public void OpenParentFolder()
    {
        string parentFolder = Directory.GetParent(currentFolder).FullName;
        OpenFolder(parentFolder);
    }
    public void RefreshFileList()
    {
        currentFolderText.text = "Current Folder: " + currentFolder;

        foreach (Transform child in fileEntryContainer)
        {
            Destroy(child.gameObject);
        }

        DisplayFolders();

        fileListingStrategy.DisplayFiles(currentFolder, fileEntryPrefab, fileEntryContainer, OpenFolder, OpenFolder);
    }

    private void DisplayFolders()
    {
        string[] directories = Directory.GetDirectories(currentFolder);

        foreach (string directory in directories)
        {
            GameObject fileEntry = Instantiate(fileEntryPrefab, fileEntryContainer);
            Image thumbnailImage = fileEntry.transform.Find("Thumbnail").GetComponent<Image>();
            thumbnailImage.gameObject.SetActive(false);

            fileEntry.GetComponent<FileInImporter>().SetUp("[" + Path.GetFileName(directory) + "]", directory);

        }
    }



    private interface IFileListingStrategy
    {
        void DisplayFiles(string folderPath, GameObject fileEntryPrefab, Transform fileEntryContainer, Action<string> fileSelectionHandler, Action<string> folderSelectionHandler);
    }

    private class PaintingStrategy : IFileListingStrategy
    {
        public void DisplayFiles(string folderPath, GameObject fileEntryPrefab, Transform fileEntryContainer, Action<string> fileSelectionHandler, Action<string> folderSelectionHandler)
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                if (IsImageFile(file))
                {
                    GameObject fileEntry = InstantiateFileEntry(fileEntryPrefab, fileEntryContainer, file, fileSelectionHandler);
                    fileEntry.GetComponent<FileInImporter>().SetUp(Path.GetFileName(file),file );
                    SetThumbnailForImageFile(fileEntry, file);
                }
            }
        }

        private GameObject InstantiateFileEntry(GameObject prefab, Transform container, string filePath, Action<string> fileSelectionHandler)
        {

            GameObject fileEntry = Instantiate(prefab, container);

            return fileEntry;
        }

        private bool IsImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png";
        }

        private void SetThumbnailForImageFile(GameObject fileEntry, string imagePath)
        {
            Image thumbnailImage = fileEntry.transform.Find("Thumbnail").GetComponent<Image>();
            Sprite thumbnail = LoadImageThumbnail(imagePath);
            thumbnailImage.sprite = thumbnail;
        }

        private Sprite LoadImageThumbnail(string imagePath)
        {
            Texture2D texture = LoadTexture(imagePath);
            if (texture != null)
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
            return null;
        }

        private Texture2D LoadTexture(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageBytes))
            {
                return texture;
            }
            else
            {
                Debug.LogWarning("Failed to load image: " + imagePath);
                return null;
            }
        }
    }

    public class PosterStrategy : IFileListingStrategy
    {
        public void DisplayFiles(string folderPath, GameObject fileEntryPrefab, Transform fileEntryContainer, Action<string> fileSelectionHandler, Action<string> folderSelectionHandler)
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                if (IsImageFile(file) && IsPosterSize(file))
                {
                    GameObject fileEntry = InstantiateFileEntry(fileEntryPrefab, fileEntryContainer, file, fileSelectionHandler);
                    fileEntry.GetComponent<FileInImporter>().SetUp(Path.GetFileName(file), file);
                    SetThumbnailForImageFile(fileEntry, file);
                }
            }
        }
        private GameObject InstantiateFileEntry(GameObject prefab, Transform container, string filePath, Action<string> fileSelectionHandler)
        {
            GameObject fileEntry = Instantiate(prefab, container);

            return fileEntry;
        }

        private bool IsPosterSize(string imagePath)
        {
            Texture2D texture = LoadTexture(imagePath);
            if (texture != null)
            {
                float aspectRatio = (float)texture.width / texture.height;
                return Mathf.Approximately(aspectRatio, 18f / 24f);
            }
            return false;
        }

        private bool IsImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png";
        }

        private void SetThumbnailForImageFile(GameObject fileEntry, string imagePath)
        {
            Image thumbnailImage = fileEntry.transform.Find("Thumbnail").GetComponent<Image>();
            Sprite thumbnail = LoadImageThumbnail(imagePath);
            thumbnailImage.sprite = thumbnail;
        }

        private Sprite LoadImageThumbnail(string imagePath)
        {
            Texture2D texture = LoadTexture(imagePath);
            if (texture != null)
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
            return null;
        }
        private Texture2D LoadTexture(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(imageBytes))
            {
                return texture;
            }
            else
            {
                Debug.LogWarning("Failed to load image: " + imagePath);
                return null;
            }
        }


    }

    private class DescriptionStrategy : IFileListingStrategy
    {
        public void DisplayFiles(string folderPath, GameObject fileEntryPrefab, Transform fileEntryContainer, Action<string> fileSelectionHandler, Action<string> folderSelectionHandler)
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLower();
                if (extension == ".txt")
                {
                    GameObject fileEntry = InstantiateFileEntry(fileEntryPrefab, fileEntryContainer, file, fileSelectionHandler);
                    fileEntry.GetComponent<FileInImporter>().SetUp(Path.GetFileName(file), file);
                }
            }

        }

        private GameObject InstantiateFileEntry(GameObject prefab, Transform container, string filePath, Action<string> fileSelectionHandler)
        {
            GameObject fileEntry = Instantiate(prefab, container);

            return fileEntry;
        }
    }


    private class SculptureStrategy : IFileListingStrategy
    {
        public void DisplayFiles(string folderPath, GameObject fileEntryPrefab, Transform fileEntryContainer, Action<string> fileSelectionHandler, Action<string> folderSelectionHandler)
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLower();
                if (extension == ".fbx")
                {
                    GameObject fileEntry = InstantiateFileEntry(fileEntryPrefab, fileEntryContainer, file, fileSelectionHandler);
                    fileEntry.GetComponent<FileInImporter>().SetUp(Path.GetFileName(file), file);
                }
            }

        }
        private GameObject InstantiateFileEntry(GameObject prefab, Transform container, string filePath, Action<string> fileSelectionHandler)
        {
            GameObject fileEntry = Instantiate(prefab, container);

            return fileEntry;
        }
        
    }


    private class MusicStrategy : IFileListingStrategy
    {
        public void DisplayFiles(string folderPath, GameObject fileEntryPrefab, Transform fileEntryContainer, Action<string> fileSelectionHandler, Action<string> folderSelectionHandler)
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLower();
                if (extension == ".wav")
                {
                    GameObject fileEntry = InstantiateFileEntry(fileEntryPrefab, fileEntryContainer, file, fileSelectionHandler);
                    fileEntry.GetComponent<FileInImporter>().SetUp(Path.GetFileName(file), file);
                }
            }

        }
        private GameObject InstantiateFileEntry(GameObject prefab, Transform container, string filePath, Action<string> fileSelectionHandler)
        {
            GameObject fileEntry = Instantiate(prefab, container);

            return fileEntry;
        }

    }
}
