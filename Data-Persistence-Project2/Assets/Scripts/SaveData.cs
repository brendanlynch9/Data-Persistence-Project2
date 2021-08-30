/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    public int bestScore;

    public void SaveBestScore(int m_BestScore)
    {
        SaveData data = new SaveData();
        data.bestScore = m_BestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public int LoadBestScore(int m_BestScore)
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return m_BestScore = data.bestScore;
        }
    }
}*/
