using Assets._Script;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public Stack<GameObject> bulletPool = new Stack<GameObject>();
    public GameObject bulletPrefab;
    public static BulletPool instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        if(bulletPool.Count == 0)
        {
            GameObject bullet = Instantiate(bulletPrefab,position, rotation,transform);
            bullet.GetComponent<Bullet>().lifeTime = bullet.GetComponent<Bullet>().lifeTImeMax;
            return bullet;
        }
        else
        {
            GameObject bullet = bulletPool.Pop();
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.GetComponent<Bullet>().lifeTime = bullet.GetComponent<Bullet>().lifeTImeMax;
            bullet.SetActive(true);
            return bullet;
        }
    }

    public void Update()
    {
        //Debug.Log(bulletPool.Count);
    }
    public void OnBulletDestroy(GameObject bullet)
    {
        bulletPool.Push(bullet);
    }
}
