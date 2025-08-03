using Assets._Script;
using System.Collections;
using UnityEngine;

public class Turel : MonoBehaviour
{
    public float Damage;
    public float AttackRate;
    public float radious;
    public float rotateSpeed;
    [SerializeField] private Transform turelPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform turelTube;
    private bool isShootable = true;
    private Quaternion turelRotation;
    private Transform currentTarget;
    private void Update()
    {
        FindEnemy();
        Rotate();
        Shoot();
    }

    private void Shoot()
    {
        if (currentTarget == null || isShootable == false)
            return;

        GameObject bullet = Instantiate(bulletPrefab, turelPoint.position, turelRotation);
        bullet.GetComponent<Bullet>().damage = Damage;

        StartCoroutine(WaitShoot());
    }

    IEnumerator WaitShoot()
    {
        isShootable = false;

        yield return new WaitForSeconds(AttackRate);

        isShootable = true;
    }

    private void Rotate()
    {
        if (currentTarget == null)
            return;

        Vector3 dir = (currentTarget.position - transform.position);
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Quaternion rotate = Quaternion.Euler(0f, angle, 0f);
        turelRotation = Quaternion.RotateTowards(turelRotation, rotate, rotateSpeed * Time.deltaTime);
        turelTube.rotation = turelRotation;
    }

    private void FindEnemy()
    {
        if (currentTarget != null)
            return;
        
        Collider[] collider = Physics.OverlapSphere(transform.position, radious);
        float min = radious;
        for(int i = 0;i < collider.Length; i++)
        {
            if (collider[i] == null)
                continue;

            if (collider[i].gameObject.GetComponent<Enemy>() != null && Vector3.Distance(transform.position, collider[i].transform.position) < min)
            {
                currentTarget = collider[i].gameObject.transform;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}