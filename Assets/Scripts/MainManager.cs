using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    public static MainManager Instance;

    public PlayerData playerData = new PlayerData();

    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        playerData.name = MenuUIScript.Instance.playerName;

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
            playerData.score = m_Points;

            if (isHighScore())
            {
                SaveHighScore();
            }

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
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public class PlayerData
    {
        public string name;
        public int score;

        public override string ToString()
        {
            if (this != null)
            {
                return name + " " + score;
            }
            else return "";
            
        }
    }

    public bool isHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            PlayerData jsonData = JsonUtility.FromJson<PlayerData>(content);

            return playerData.score > jsonData.score;
        }
        else
        {
            return true;
        }
    }

    public void SaveHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        string createdJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, createdJson);
    }

    public static string ReturnHighScoreAndName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        PlayerData jsonData = null;

        print(File.Exists(path));


        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            jsonData = JsonUtility.FromJson<PlayerData>(content);
        }

        return jsonData.ToString();
    }


}
