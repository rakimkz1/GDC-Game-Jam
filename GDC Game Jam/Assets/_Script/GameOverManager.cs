using Assets._Script;
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
        int bestScore = PlayerPrefs.GetInt("BestScore");
        PlayerPrefs.SetInt("BestScore", Mathf.Max(bestScore, EnemySpawner.instance.killScore));
        Time.timeScale = 0f;
    }

    public void OnRestartGame()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        PlayerPrefs.SetInt("BestScore", Mathf.Max(bestScore, EnemySpawner.instance.killScore));
        SceneManager.LoadScene(1);
    }

    public void OnMenu()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        PlayerPrefs.SetInt("BestScore", Mathf.Max(bestScore, EnemySpawner.instance.killScore));
        SceneManager.LoadScene(0);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}