using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataHandler : IDataHandler
{
    public bool SaveData<T>(string Path, T Data)
    {
        string dataPath = Application.persistentDataPath + Path;

        if (File.Exists(dataPath))
        {
            try
            {
                File.Delete(dataPath);

            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        using FileStream stream = File.Create(dataPath);
        stream.Close();

        File.WriteAllText(dataPath, JsonConvert.SerializeObject(Data));
        Debug.Log("                          " + "SAVED" + Path + "         " + Data);
        return true;

    }

    public T LoadData<T>(string Path)
    {
        string dataPath = Application.persistentDataPath + Path;

        if (!File.Exists(dataPath))
        {
            Debug.Log("File doesn't exist. " + dataPath);
            throw new FileNotFoundException($"{dataPath} does not exist.");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(dataPath));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    public List<string> FindJsonFilesInDirectory(string directoryPath)
    {
        string dataPath = Application.persistentDataPath + directoryPath;
        List<string> jsonFilePaths = new List<string>();

        if (Directory.Exists(dataPath))
        {
            string[] files = Directory.GetFiles(dataPath, "*.json", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string cutfile = file.Replace(Application.persistentDataPath,"");
                jsonFilePaths.Add(cutfile);
            }
        }
        else
        {
            Debug.LogError("Directory does not exist: " + dataPath);
        }

        return jsonFilePaths;
    }

    public void DeleteAllFilesInDirectory(string directoryPath)
    {
        string dataPath = Application.persistentDataPath + directoryPath;
        if (Directory.Exists(dataPath))
        {
            string[] files = Directory.GetFiles(dataPath);

            foreach (string file in files)
            {
                File.Delete(file);
                Debug.Log("Deleted file: " + file);
            }
        }
        else
        {
            Debug.LogError("Directory does not exist: " + dataPath);
        }
    }

    public void CreateDirectory(string directoryPath)
    {
        string dataPath;
        if (directoryPath.StartsWith(Application.persistentDataPath))
        {
            dataPath = directoryPath;
        }
        else  dataPath = Application.persistentDataPath + directoryPath;
        try
            {

                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                    Debug.Log("Directory created successfully at path: " + dataPath);
                }
                else
                {
                    Debug.LogWarning("Directory already exists at path: " + dataPath);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error creating directory: " + ex.Message);
            }

    }
    public void CopyFileIntoAppData(string targetPath, string fileToCopyPath)
    {
        try
        {
            if (!Directory.Exists(targetPath)) CreateDirectory(targetPath);
            if (File.Exists(fileToCopyPath))
            {
                string fileName = Path.GetFileName(fileToCopyPath);
                string dataPath = Application.persistentDataPath + targetPath;
                string destinationFilePath = Path.Combine(dataPath, fileName);

                File.Copy(fileToCopyPath, destinationFilePath, true);

                Debug.Log("File copied successfully.");
            }
            else
            {
                Debug.LogError("File to copy does not exist.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error copying file: " + ex.Message);
        }
    }
}

