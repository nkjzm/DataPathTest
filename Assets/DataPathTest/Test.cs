using UnityEngine;
using System.IO;

public class Test : MonoBehaviour
{
    [SerializeField]
    GameObject PanelPrefab;
    [SerializeField]
    GameObject DatePrefab;
    [SerializeField]
    Transform Contents;

    const string EXIST = "存在する.";
    const string NO_EXIST = "存在しない.";

    public void CreatePanelAll()
    {
        var go = Instantiate(DatePrefab);
        go.transform.SetParent(Contents);
        go.GetComponent<UnityEngine.UI.Text>().text = "Created: " + System.DateTime.Now;

        CreatePanelByLoad("DataPath", Application.dataPath);
        CreatePanelByLoad("PersistentDataPath", Application.persistentDataPath);
        CreatePanelByLoad("TemporaryCachePath", Application.temporaryCachePath);
        CreatePanelByLoad("StreamingAssetsPath", Application.streamingAssetsPath);
        CreatePanel().GetComponent<Panel>().Set("PlayerPrefs: ", PlayerPrefs.GetString("test", NO_EXIST), "unknown");
    }

    public void DestroyPanelAll()
    {
        foreach (Transform item in Contents)
        {
            Destroy(item.gameObject);
        }
    }

    public void SaveFileAll()
    {
        SaveFile(Application.dataPath);
        SaveFile(Application.persistentDataPath);
        SaveFile(Application.temporaryCachePath);
        SaveFile(Application.streamingAssetsPath);
        PlayerPrefs.SetString("test", EXIST);
        PlayerPrefs.Save();
    }

    public void DeleteFileAll()
    {
        DeleteFile(Application.dataPath);
        DeleteFile(Application.persistentDataPath);
        DeleteFile(Application.temporaryCachePath);
        DeleteFile(Application.streamingAssetsPath);
        PlayerPrefs.DeleteAll();
    }

    void DeleteFile(string rootPath)
    {
        File.Delete(rootPath + "/test");
    }

    void CreatePanelByLoad(string label, string path)
    {
        CreatePanel().GetComponent<Panel>().Set(label + ": ", LoadFile(path), path);
    }

    GameObject CreatePanel()
    {
        var panel = Instantiate(PanelPrefab);
        panel.transform.SetParent(Contents);
        return panel;
    }

    void SaveFile(string rootPath)
    {
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }
        string path = rootPath + "/test";
        File.WriteAllText(path, EXIST, System.Text.Encoding.UTF8);
    }

    string LoadFile(string rootPath)
    {
        string path = rootPath + "/test";
        if (!File.Exists(@path))
        {
            return NO_EXIST;
        }
        return File.ReadAllText(path, System.Text.Encoding.UTF8);
    }
}
