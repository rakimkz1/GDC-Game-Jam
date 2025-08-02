using UnityEngine;

namespace Assets._Script
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody rb;
        public float speed;
        public float lifeTime;
        public float damage;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        public void Update ()
        {
            Move();
            lifeTime -= Time.deltaTime;
            if(lifeTime < 0f)
                Destroy(gameObject);
        }

        private void Move()
        {
            rb.velocity = transform.forward * speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider != null && collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().Damage(damage);
                Destroy(gameObject);
            }
        }
    }
}