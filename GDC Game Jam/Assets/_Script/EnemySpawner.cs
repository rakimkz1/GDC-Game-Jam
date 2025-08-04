using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets._Script
{
    public class EnemySpawner : MonoBehaviour
    {
        public event Action OnAllEnemyKill;
        public float minWait;
        public float maxWait;
        public float spawnDistance;
        public float minSpawnAngle;
        public float maxSpawnAngle;
        public int killScore;
        [SerializeField] private Whale whale;
        [SerializeField] private Transform whalePos;
        [SerializeField] private GameObject[] enemyPrefab;
        [SerializeField] private TextMeshProUGUI txt_KillScore;
        public static EnemySpawner instance;
        public void Start()
        {
            instance = this;
            whale.OnGameOver += () => StopAllCoroutines();
        }

        public IEnumerator SpawnEnemy(float min,float max, int number)
        {
            int i = 0;
            while (i < number)
            {
                float random = UnityEngine.Random.Range(min, max);
                float angle = UnityEngine.Random.Range(minSpawnAngle, maxSpawnAngle);
                Vector3 pos = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * spawnDistance + Vector3.up;
                yield return new WaitForSeconds(random);

                Vector3 dir = whalePos.position - pos;
                float spawnAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                GameObject target = GetEnemy(pos, spawnAngle);
                target.GetComponent<Enemy>().OnEnemyDead += GetComponent<RoundSystem>().OnEnemyDead;
                target.GetComponent<Enemy>().OnEnemyKilled += OnEnemyKilled;
                target.GetComponent<Enemy>().whale = whale;
                target.GetComponent<Enemy>().target = whalePos;
                OnAllEnemyKill += () => target.GetComponent<Enemy>().DestroyEnemy(false);
                i++;
            }
        }

        private GameObject GetEnemy(Vector3 pos, float spawnAngle)
        {
            int index = (UnityEngine.Random.Range(0, 10)) < 9 ? 0 : 1; 
            return Instantiate(enemyPrefab[index], pos, Quaternion.Euler(0f, spawnAngle, 0f));
        }

        public void OnEnemyKilled()
        {
            killScore++;
            txt_KillScore.text = killScore.ToString();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(transform.position, new Vector3(Mathf.Cos(minSpawnAngle), 0f, Mathf.Sin(minSpawnAngle)) * spawnDistance + transform.position);
            Gizmos.DrawLine(transform.position, new Vector3(Mathf.Cos(maxSpawnAngle), 0f, Mathf.Sin(maxSpawnAngle)) * spawnDistance + transform.position);
        }
    }
}