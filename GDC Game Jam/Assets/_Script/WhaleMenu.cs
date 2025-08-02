using JetBrains.Annotations;
using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;


public class WhaleMenu : MonoBehaviour
{
    public bool isAproach;
    public float radious;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private TextMeshPro upgrate_txt;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Whale whale;
    public BuyType currentType;
    public int currectIndex;
    public int[] BuyCosts;
    public string[] upgrateTexts;
    public int bulletNumber;
    public int HealAmount;
    private void Update()
    {
        CheckPlayer();

        if (isAproach)
        {
            ChangeBuyType();
            BuyUpgrate();
        }
    }

    private void BuyUpgrate()
    {
        if (Input.GetKeyDown(KeyCode.E) && ResourceColection.instance.resourceNumber >= BuyCosts[currectIndex])
        {
            switch (currentType)
            {
                case BuyType.Bullet:
                    GetBullet();
                    break;
                case BuyType.Heal:
                    HealWhale();
                    break;
                case BuyType.UpgrateAttack:
                    UpgrateAttack();
                    break;
                case BuyType.UpgrateAttackSpeed:
                    UpgrateAttackSpeed();
                    break;
                default:
                    break;
            }
        }
    }

    private void UpgrateAttackSpeed()
    {
        playerAttack.attackRate *= 0.85f;
        ResourceColection.instance.resourceNumber -= BuyCosts[(int)BuyType.UpgrateAttackSpeed];
    }

    private void UpgrateAttack()
    {
        playerAttack.damage *= 1.3f;
        ResourceColection.instance.resourceNumber -= BuyCosts[((int)BuyType.UpgrateAttack)];
    }

    private void HealWhale()
    {
        whale.HealWhale(HealAmount);
        ResourceColection.instance.resourceNumber -= BuyCosts[(int)BuyType.UpgrateAttackSpeed];
    }

    private void GetBullet()
    {
        playerAttack.GetBullet(bulletNumber);
        ResourceColection.instance.resourceNumber -= BuyCosts[(int)BuyType.Bullet];
    }


    private void ChangeBuyType()
    {
        currectIndex = Mathf.Clamp(currectIndex + (int)Input.mouseScrollDelta.y, 0, BuyCosts.Length);
        currentType = (BuyType)Mathf.Clamp(currectIndex + (int)Input.mouseScrollDelta.y, 0, BuyCosts.Length);
        upgrate_txt.text = upgrateTexts[currectIndex] + BuyCosts[currectIndex];
    }

    private void CheckPlayer()
    {
        Collider[] coliders = Physics.OverlapSphere(transform.position, radious);

        for (int i = 0; i < coliders.Length; i++)
        {
            if (coliders[i].gameObject != null && coliders[i].gameObject.tag == "Player" && isAproach == false)
            {
                Activate();
                return;
            }
            if (coliders[i].gameObject != null && coliders[i].gameObject.tag == "Player")
                return;
        }
        if (isAproach == true)
            Deactivate();
    }

    private void Deactivate()
    {
        isAproach = false;
        menuCanvas.SetActive(false);
    }

    private void Activate()
    {
        isAproach = true;
        menuCanvas.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radious);
    }   
}

public enum BuyType
{
    Bullet,
    Heal,
    UpgrateAttack,
    UpgrateAttackSpeed
}