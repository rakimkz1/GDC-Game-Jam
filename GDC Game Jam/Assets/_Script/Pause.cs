using System;
using Assets._Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject button;
    public GameObject panel;
    
    private bool isinpause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isinpause)
                ResumeGame();
            else 
                PauseGame();
        }
    }

    public void PauseGame()
    {
        //Freeze true
        button.SetActive(false);
        Time.timeScale = 0f;
        panel.SetActive(true);
        isinpause = true;
    }

    public void ResumeGame()
    {
        // Freeze false
        button.SetActive(true);
        Time.timeScale = 1f;
        panel.SetActive(false);
        isinpause = false;
    }

    public void Menu()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        PlayerPrefs.SetInt("BestScore",Mathf.Max (bestScore, EnemySpawner.instance.killScore));
        SceneManager.LoadScene(0);
    }
	
	
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
