using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public event Action OnEnemyDead;

    public Transform target;
    public float hp;
    public float speed;
    public float damage;
    public Whale whale;
    public float lifeTime;
    public float pushForce;
    public float stanTime;
    public RoundSystem round;
    private bool isStaned;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        whale.OnGameOver += DestroyEnemy;
    }
    void Update()
    {
        if(isStaned == false) rb.velocity = transform.forward * speed;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f) DestroyEnemy();
    }

    public void Damage(float damage)
    {
        hp-=damage;
        rb.AddForce((target.position - transform.position).normalized * pushForce);
        if (hp <= 0f)
            DestroyEnemy(true);
        StopCoroutine(StanTime());
        StartCoroutine(StanTime());
    }

    public void DestroyEnemy(bool addResource)
    {
        if(addResource) 
            AddResource();
        whale.OnGameOver -= DestroyEnemy;
        OnEnemyDead?.Invoke();

        Destroy(gameObject);
    }
    public void DestroyEnemy()
    {
        DestroyEnemy(false);
    }

    IEnumerator StanTime()
    {
        isStaned = true;
        yield return new WaitForSeconds(stanTime);
        isStaned = false;
    }

    private void AddResource()
    {
        int number = UnityEngine.Random.Range(1, 6);
        ResourceColection.instance.AddResource(number);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "whale")
        {
            whale.TakeDamage(damage);
            if(gameObject != null)
                DestroyEnemy();
        }
    }
}