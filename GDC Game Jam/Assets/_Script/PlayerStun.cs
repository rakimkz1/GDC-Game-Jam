using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public float Force;
    public float ForceStuningTime;
    public float StuningTime;
    public float PostStunedTime;
    public bool isStunable;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip stunAudio;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public async Task Stun(Transform pos)
    {
        GetComponent<PlayerAttack>().shootable = false;
        GetComponent<PlayerMove>().isMoveable = false;
        isStunable = false;
        anim.SetBool("isStuned", true);
        AudioManager.instance.Play(stunAudio);
        Vector3 dir = -(pos.position - transform.position);
        if(Vector3.Distance(dir, pos.right) < Vector3.Distance(dir, -pos.right))
            dir = (2f * dir.normalized + pos.right).normalized * Force;
        else
            dir = (2f * dir.normalized - pos.right).normalized * Force;
        rb.velocity = dir;
        await Task.Delay((int)(ForceStuningTime * 1000f));
        rb.velocity = Vector3.zero;
        await Task.Delay((int)(StuningTime * 1000f));
        anim.SetBool("isStuned", false);
        GetComponent<PlayerAttack>().shootable = true;
        GetComponent<PlayerMove>().isMoveable = true;
        await Task.Delay((int)(PostStunedTime * 1000f));
        isStunable = true;
    }
}
