using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public string playerName;
    public string highScoreName;
    public int highScore;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            LoadScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string HighScoreName;
        public int HighScore;
    }

    public void UpdateRecordScore(int score)
    {
        highScore = score;
        highScoreName = playerName;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();

        data.HighScoreName = highScoreName;
        data.HighScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/SaveScore.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/SaveScore.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.HighScoreName;
            highScore = data.HighScore;
        }
    }
}
