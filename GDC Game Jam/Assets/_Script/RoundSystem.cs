using Assets._Script;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    public int currentRound;
    public int enemyNumber;
    public float enemyProportion;
    public float spawnTimeProportion;
    public float waitAfterRound;
    public bool isBuymentTime;
    private EnemySpawner enemySpawner;
    private int currentEnemyNumber;
    [SerializeField] private Whale whale;
    [SerializeField] private TextMeshProUGUI textCounter;
    [SerializeField] private TextMeshProUGUI txt_Round;
    private void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        currentEnemyNumber = enemyNumber;
        whale.OnGameOver += OnGameOver;
        txt_Round.text = $"Round : {currentRound + 1}";
        StartCoroutine(enemySpawner.SpawnEnemy(enemySpawner.minWait, enemySpawner.maxWait, enemyNumber));
    }

    private void Round()
    {
        currentRound++;
        int enemy = (int)(enemyNumber * Mathf.Pow(enemyProportion, currentRound / 2));
        float maxTime = (enemySpawner.maxWait * Mathf.Pow(spawnTimeProportion, currentRound / 3));
        float minTime = (enemySpawner.minWait * Mathf.Pow(spawnTimeProportion, currentRound / 5));

        txt_Round.text = $"Round : {currentRound + 1}";
        currentEnemyNumber = enemy;
        StartCoroutine(enemySpawner.SpawnEnemy(minTime, maxTime, enemy));
    }

    private void OnGameOver()
    {
        StopAllCoroutines();
    }

    public void OnEnemyDead()
    {
        currentEnemyNumber--;
        if (currentEnemyNumber == 0)
        {
            StartCoroutine(WaitRound());
        }
            
    }
    
    IEnumerator WaitRound()
    {
        isBuymentTime = true;
        float time = waitAfterRound;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            textCounter.text = ((int)time).ToString();
            yield return new WaitForEndOfFrame();
        }
        textCounter.text = "";
        isBuymentTime = false;
        Round();
    }

    private void OnDisable()
    {
        whale.OnGameOver -= OnGameOver;
    }
}