using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public Tank tank;

    public float spawnTime;

    float m_spawnTime;

    bool isGameOver;

    public float maxBackgroundX = 40;
    public float maxBackgroundY = 25;

    public int highScore;
    public int score;

    UIManager ui;

	// Start is called before the first frame update
	void Start()
    {
        m_spawnTime = 0.5f;
        ReadHighScore();
        tank = FindObjectOfType<Tank>();
        ui = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tank.IsDie())
        {
            GameOver();
        }
        m_spawnTime -= Time.deltaTime;
        if(m_spawnTime <= 0)
        {
            SpawnEnemy();
            m_spawnTime = spawnTime;
        }
        UpdateHighScore(score);
        ui.SetHighScoreText("High Score: " + highScore.ToString());
        ui.SetScoreText("Score: " + score.ToString());  
    }

    private void ReadHighScore()
    {
		string filePath = Application.dataPath + "/Data/highscore.txt";
		if (File.Exists(filePath))
		{
			StreamReader reader = new StreamReader(filePath);
			string scoreString = reader.ReadLine();
			int.TryParse(scoreString, out highScore);
			reader.Close();
		}
	}

	public void UpdateHighScore(int newScore)
	{
		if (newScore > highScore)
		{
			highScore = newScore;
			StreamWriter writer = new StreamWriter(Application.dataPath + "/Data/highscore.txt");
			writer.Write(highScore);
			writer.Close();
		}
	}

    public void IncreaseScore(int _score)
    {
        score += _score;
    }
	public void SpawnEnemy()
    {
        float randXpos1 = Random.Range(tank.transform.position.x +(-11f), tank.transform.position.x + 11f);
		Vector2 spawnPos1 = new Vector2(randXpos1, tank.transform.position.y + 5f);
		Vector2 spawnPos2 = new Vector2(randXpos1, tank.transform.position.y - 5f);
		if (enemy)
        {
            Instantiate(enemy, spawnPos1, Quaternion.identity);
            Instantiate(enemy, spawnPos2, Quaternion.identity);
		}
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
