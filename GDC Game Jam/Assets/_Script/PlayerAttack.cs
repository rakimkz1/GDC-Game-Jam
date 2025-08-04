using Assets._Script;
using System.Collections;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(PlayerMove))]
public class PlayerAttack : MonoBehaviour
{
    public float attackRate;
    public int bulletNumber;
    public float damage;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootStartPoint;
    [SerializeField] private TextMeshProUGUI bullet_txt;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip lazerSound;
    private PlayerMove playerMove;
    public bool shootable = true;

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        ShowBulletNumber();
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if(shootable && Input.GetMouseButton(0) && bulletNumber > 0)
        {
            StartCoroutine(shootColdown());
            Vector3 diraction = playerMove.Diraction;
            float angle = Mathf.Atan2(diraction.x, diraction.z) * Mathf.Rad2Deg;
            Quaternion rotate = Quaternion.Euler(0f, angle, 0f);
            GameObject target = BulletPool.instance.GetBullet(shootStartPoint.position, rotate);    
            target.GetComponent<Bullet>().damage = damage;
            bulletNumber --;
            anim.SetTrigger("Shoot");
            AudioManager.instance.Play(lazerSound);
            ShowBulletNumber();
        }
    }
    public void ShowBulletNumber() => bullet_txt.text = bulletNumber.ToString();

    public void GetBullet(int number)
    {
        bulletNumber += number;
        ShowBulletNumber();
    }

    IEnumerator shootColdown()
    {
        shootable = false;
        yield return new WaitForSeconds(attackRate);
        shootable = true;
    }
}