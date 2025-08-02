using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._Script
{
    public class EnemySpawner : MonoBehaviour
    {
        public float min;
        public float max;
        public float spawnDistance;
        public float minSpawnAngle;
        public float maxSpawnAngle;
        [SerializeField] private Whale whale;
        [SerializeField] private Transform whalePos;
        [SerializeField] private GameObject enemyPrefab;

        public void Start()
        {
            whale.OnGameOver += () => StopAllCoroutines();
            StartCoroutine(SpawnEnemy());
        }

        IEnumerator SpawnEnemy()
        { 
            float random = Random.Range(min, max);
            float angle = Random.Range(minSpawnAngle, maxSpawnAngle);
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * spawnDistance + Vector3.up;
            yield return new WaitForSeconds(random);


            Vector3 dir = whalePos.position - pos;
            float spawnAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            GameObject target = Instantiate(enemyPrefab, pos, Quaternion.Euler(0f, spawnAngle, 0f));
            target.GetComponent<Enemy>().whale = whale;
            target.GetComponent<Enemy>().target = whalePos;

            StartCoroutine(SpawnEnemy());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(transform.position, new Vector3(Mathf.Cos(minSpawnAngle), 0f, Mathf.Sin(minSpawnAngle)) * spawnDistance + transform.position);
            Gizmos.DrawLine(transform.position, new Vector3(Mathf.Cos(maxSpawnAngle), 0f, Mathf.Sin(maxSpawnAngle)) * spawnDistance + transform.position);
        }
    }
}