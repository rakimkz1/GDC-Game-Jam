using Assets._Script;
using DG.Tweening;
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
    [SerializeField] private TextMeshProUGUI txt_NextRound;
    [SerializeField] private AudioClip notification;
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
            NextRound();
        }
            
    }
    
    private void NextRound()
    {
        txt_NextRound.gameObject.SetActive(true);
        AudioManager.instance.Play(notification);
        txt_NextRound.text = $"Round : {currentRound + 1}";

        //DOTween sequence = DOTween.Sequence();
        //sequence.

        txt_NextRound.gameObject.transform.DOScale(1f, 0.6f).From(0f).SetEase(Ease.InExpo).SetDelay(100f).OnComplete(() =>
        {
            txt_NextRound.gameObject.transform.DOScale(0f, 0.6f).SetEase(Ease.InExpo).OnComplete(() => StartCoroutine(WaitRound()));
            txt_NextRound.gameObject.SetActive(false);
        });
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