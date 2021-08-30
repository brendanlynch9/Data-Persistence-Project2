using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////////////
    [System.Serializable]
    class SaveData
    {
        public string PlayerNameBestScore;
        public int bestScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        //if (data.bestScore < m_BestScore)
        //{
            data.bestScore = m_BestScore;
            data.PlayerNameBestScore = MenuManager.Instance.nameText.text + data.bestScore;
        //}
        /////////////////////////////
        //data.PlayerNameBestScore = MenuManager.Instance.nameText.text + data.bestScore; //m_BestScore; //BestScore.text;
        Debug.Log("Save data: " + data.PlayerNameBestScore);
        /////////////////////////////
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

            m_BestScore = data.bestScore;
            /////////////////////////////
            //MenuManager.Instance.nameText.text = data.PlayerNameBestScore; 
            BestScore.text = data.PlayerNameBestScore;
            /////////////////////////////
        }
    }
    /////////////////////////////////////////////////////////////////////////////////
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    //public static MainManager Instance;
    public TextMeshProUGUI BestScore;
    /////////////////////////// 
    private int temp_BestScore;
    ///////////////////////////

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int m_BestScore;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {

        ///////////////////////////////////////////
        LoadBestScore();
        //BestScore.text = MenuManager.Instance.nameText.text;
        /////////////////////////////////////////////
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_BestScore = m_Points;
        //BestScore.text += " " + m_BestScore;
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            temp_BestScore = data.bestScore;
        }
        if (temp_BestScore < m_Points)
        {
            SaveBestScore();
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
