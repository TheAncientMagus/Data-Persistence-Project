using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class SaveUserScore : MonoBehaviour
{
    public static SaveUserScore instance;
    public Text HighScore;

    public int score;
    public int highScore;
    public string userName;
    public string topUserName;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
        HighScore.text = $"High Score: {topUserName}: {highScore}";
    }

    public void SaveUserName(string name)
    {
        userName = name;
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string topUserName;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.topUserName = topUserName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            topUserName = data.topUserName;
        }
    }
}
