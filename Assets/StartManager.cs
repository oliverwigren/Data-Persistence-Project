using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StartManager : MonoBehaviour
{
    public static StartManager Instance;
    public string name;
    public int highScore;
    [SerializeField] private InputField input;
    [SerializeField] private Text scoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        name = input.text;
    }
    public void Play()
    {
        if (name != null)
        {
        SaveName();
        }
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

        [System.Serializable]
    class SaveData
    {
        public string name;
        public int highScore;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.name = name;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            name = data.name;
            highScore = data.highScore;
        }
    }
}
