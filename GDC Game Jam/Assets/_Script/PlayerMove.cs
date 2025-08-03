using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed;
    public Vector3 Diraction;
    public bool isMoveable;
    public float moveSoundRate;
    [SerializeField] private GameObject playerBody;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip[] moveSound;
    [SerializeField, Range(0f, 1f)] private float moveSoundVolume;
    private Rigidbody rb;
    private bool isMoveSound = true;

    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update ()
    {
        Move();
        FindDiraction();
        Rotate();
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(Diraction.x, Diraction.z) * Mathf.Rad2Deg;
        playerBody.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void FindDiraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Diraction = (hit.point - transform.position).normalized;
        Diraction.y = 0f;
        Diraction = Diraction.normalized;
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if(isMoveable)
        {
            rb.velocity = new Vector3(x, 0f, y) * Speed;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Speed);
        }

        if (x != 0f || y != 0f)
        {
            anim.SetBool("Run", true);
            if (isMoveSound)
                StartCoroutine(WalkSoundDeley());
        }
        else
            anim.SetBool("Run", false);
    }

    IEnumerator WalkSoundDeley()
    {
        isMoveSound = false;
        AudioManager.instance.Play(moveSound[UnityEngine.Random.Range(0, moveSound.Length)], moveSoundVolume);
        yield return new WaitForSeconds(moveSoundRate);
        isMoveSound = true;
    }
}