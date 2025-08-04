using TMPro;
using UnityEngine;


public class WhaleMenu : MonoBehaviour
{
    public bool isAproach;
    public float radious;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private TextMeshProUGUI cost_txt;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Whale whale;
    [SerializeField] private RoundSystem round;
    [SerializeField] private AudioClip onClickSound;
    [SerializeField] private AudioClip onPanelOpen;
    [SerializeField] private AudioClip onErrorButton;
    public BuyType currentType;
    public int currectIndex;
    public int[] BuyCosts;
    public GameObject[] upgratePanel;
    public int bulletNumber;
    public int HealAmount;
    public float attackDamageProportion;
    public float attackSpeedProportion;
    public GameObject turelPrefab;
    public Transform[] turelPoint;
    private int turelNumber;
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
        if (Input.GetKeyDown(KeyCode.E))
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
                case BuyType.Turel:
                    TurelType();
                    break;
                default:
                    break;
            }
        }
    }

    public void UpgrateAttackSpeed()
    {
        if (ResourceColection.instance.resourceNumber < BuyCosts[currectIndex])
        {
            AudioManager.instance.Play(onErrorButton);
            return;
        }

        playerAttack.attackRate *= attackSpeedProportion;
        ResourceColection.instance.RemoveResource(BuyCosts[((int)BuyType.UpgrateAttackSpeed)]);
        AudioManager.instance.Play(onClickSound);
    }

    public void UpgrateAttack()
    {
        if (ResourceColection.instance.resourceNumber < BuyCosts[currectIndex])
        {
            AudioManager.instance.Play(onErrorButton);
            return;
        }

        playerAttack.damage *= attackDamageProportion;
        ResourceColection.instance.RemoveResource(BuyCosts[((int)BuyType.UpgrateAttack)]);
        AudioManager.instance.Play(onClickSound);
    }

    public void HealWhale()
    {
        if (ResourceColection.instance.resourceNumber < BuyCosts[currectIndex])
        {
            AudioManager.instance.Play(onErrorButton);
            return;
        }

        whale.HealWhale(HealAmount);
        ResourceColection.instance.RemoveResource(BuyCosts[((int)BuyType.Heal)]);
        AudioManager.instance.Play(onClickSound);
    }

    public void GetBullet()
    {
        if (ResourceColection.instance.resourceNumber < BuyCosts[currectIndex])
        {
            AudioManager.instance.Play(onErrorButton);
            return;
        }

        playerAttack.GetBullet(bulletNumber);
        ResourceColection.instance.RemoveResource(BuyCosts[((int)BuyType.Bullet)]);
        AudioManager.instance.Play(onClickSound);
    }


    private void ChangeBuyType()
    {
        upgratePanel[currectIndex].SetActive(false);
        int i = Mathf.Clamp(currectIndex + (int)Input.mouseScrollDelta.y, 0, BuyCosts.Length - 1);
        if (Input.GetKeyDown(KeyCode.R))
        {
            i = i + 1 >= BuyCosts.Length ? 0 : i + 1;
        }
        currectIndex = i;
        currentType = (BuyType)i;
        cost_txt.text = BuyCosts[currectIndex].ToString();
        upgratePanel[currectIndex].SetActive(true);
        if (Mathf.Clamp(Input.mouseScrollDelta.y + currectIndex, 0, BuyCosts.Length -1) != currectIndex)
            AudioManager.instance.Play(onPanelOpen);
    }

    public void TurelType()
    {

        if(turelNumber == turelPoint.Length || ResourceColection.instance.resourceNumber < BuyCosts[currectIndex])
        {
            AudioManager.instance.Play(onErrorButton);
            return;
        }

        SetTurel();
        ResourceColection.instance.RemoveResource(BuyCosts[(int)BuyType.Turel]);
        AudioManager.instance.Play(onClickSound);
    }

    private void SetTurel()
    {
        Instantiate(turelPrefab, turelPoint[turelNumber].position, Quaternion.identity);
        turelNumber++;
    }

    private void CheckPlayer()
    {

        if (round.isBuymentTime == false)
        {
            Deactivate();
            return;
        }

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
        AudioManager.instance.Play(onPanelOpen, 0.6f);
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
    UpgrateAttackSpeed,
    Turel
}