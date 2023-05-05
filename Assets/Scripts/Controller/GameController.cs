using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject tank;

    public float spawnTime;

    float m_spawnTime;

    bool isGameOver;

    public float maxBackgroundX = 40;
    public float maxBackgroundY = 25;

	// Start is called before the first frame update
	void Start()
    {
        m_spawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_spawnTime -= Time.deltaTime;
        if(m_spawnTime <= 0)
        {
            SpawnEnemy();
            m_spawnTime = spawnTime;
        }
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

    public void SetGameOverState(bool state)
    {
        isGameOver = state;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
