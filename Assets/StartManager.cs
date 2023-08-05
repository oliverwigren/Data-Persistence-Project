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
    public string nameNow;
    public string nameBest;
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

    private void Start()
    {
        StartManager.Instance.LoadName();
        scoreText.text = "High Score: " + StartManager.Instance.highScore;
    }

    private void Update()
    {
        nameNow = input.text;
    }
    public void Play()
    {
        if (input.text != null)
        {
            StartManager.Instance.nameNow = nameNow;
            SaveName();
        }
        else
        {
            StartManager.Instance.nameNow = "Player";
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
        public string nameNow;
        public string nameBest;
        public int highScore;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.nameNow = nameNow;
        data.nameBest = nameBest;
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

            nameNow = data.nameNow;
            nameBest = data.nameBest;
            highScore = data.highScore;
        }
    }
}
