using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paused : MonoBehaviour
{
	public GameObject PausedMenu;
	public bool isPaused=false;
	// Start is called before the first frame update
	void Start()
    {
			
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (isPaused==true)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
	}
	public void PauseGame()
	{
		PausedMenu.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}
	public void ResumeGame()
	{
		PausedMenu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}


}
