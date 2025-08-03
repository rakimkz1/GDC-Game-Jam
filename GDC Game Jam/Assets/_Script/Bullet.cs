using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Script
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField] private GameObject explotion;
        public float speed;
        public float lifeTime;
        public float lifeTImeMax;
        public float damage;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            explotion.transform.SetParent(transform, true);
        }

        public void Update ()
        {
            Move();
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0f)
                BulletDestroy();
        }

        private void Move()
        {
            rb.velocity = transform.forward * speed;
        }

        private void BulletDestroy()
        {
            BulletPool.instance.OnBulletDestroy(gameObject);
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider != null && collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().Damage(damage);
                BulletDestroy();
            }
        }
    }
}