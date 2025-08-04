using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject button;
    public GameObject panel;
	
    public void PauseGame()
    {
        //Freeze true
        button.SetActive(false);
        panel.SetActive(true);
    }

    public void ResumeGame()
    {
        // Freeze false
        button.SetActive(true);
        panel.SetActive(false);
    }

    public void Menu()
    {
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
