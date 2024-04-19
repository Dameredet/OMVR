using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ResourceFromPath : MonoBehaviour
{
    private static ResourceFromPath instance;

    public static ResourceFromPath Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ResourceFromPath>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(ResourceFromPath).Name);
                    instance = singletonObject.AddComponent<ResourceFromPath>();
                }
            }
            return instance;
        }
    }
    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Sprite SpriteFromPath(string Path)
    {
        string dataPath;
        if (!string.IsNullOrEmpty(Path)) { 
            if (!Path.Contains(Application.persistentDataPath))
            {
                dataPath = Application.persistentDataPath + Path;
            }
            else
            { 
            dataPath= Path;
            }

        Texture2D texture = LoadTexture(dataPath);

            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                return sprite;
            }
            else
            {
                Debug.LogError("Failed to load texture from path: " + dataPath);
                return null;
            }
        }
        return null;
    }

    public Texture2D LoadTexture(string dataPath)
    {
        byte[] fileData = File.ReadAllBytes(dataPath);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        return texture;
    }

}
