using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;

    public static GameOverManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}