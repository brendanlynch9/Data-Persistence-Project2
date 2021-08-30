using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class MenuUiHandler : MonoBehaviour
{
    ////////////////////////////////////////////////////////////
    [System.Serializable]
    class SaveData
    {
        public string PlayerNameBestScore;
        public int bestScore;
    }
    ////////////////////////////////////////////////////////////

    //public SaveData ManageData;
    public string input;
    public TextMeshProUGUI userName;
    public int menuBestScore;
    void Start()
    {
        //menuBestScore = ManageData.LoadBestScore();
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            userName.text = data.PlayerNameBestScore;
            //menuBestScore = data.bestScore;
        }
    }

    public void LoadMainScene(string scenename)
    {
        Debug.Log("sceneName to load: " + scenename);
        SceneManager.LoadScene(scenename);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("ExitGame");
    }

    public void GetInputString(string s)
    {
        userName.text = s + "'s Best Score: " + menuBestScore;
        //MainManager.Instance.ScoreText.text = userName.text;
        MenuManager.Instance.nameText.text = s + "'s Best Score: "; //+ menuBestScore; //userName.text;
        input = s;
        Debug.Log("Input: " + userName.text);
    }
}
