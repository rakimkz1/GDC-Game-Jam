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
                case BuyType.Turel:
                    TurelType();
                    break;
                default:
                    break;
            }
        }
    }

    private void UpgrateAttackSpeed()
    {
        playerAttack.attackRate *= 0.93f;
        ResourceColection.instance.RemoveResource(BuyCosts[((int)BuyType.UpgrateAttackSpeed)]);
    }

    private void UpgrateAttack()
    {
        playerAttack.damage *= 1.05f;
        ResourceColection.instance.RemoveResource(BuyCosts[((int)BuyType.UpgrateAttack)]);
    }

    private void HealWhale()
    {
        whale.HealWhale(HealAmount);
        ResourceColection.instance.RemoveResource(BuyCosts[((int)BuyType.Heal)]);
    }

    private void GetBullet()
    {
        playerAttack.GetBullet(bulletNumber);
        ResourceColection.instance.RemoveResource(BuyCosts[((int)BuyType.Bullet)]);
    }


    private void ChangeBuyType()
    {
        upgratePanel[currectIndex].SetActive(false);
        currectIndex = Mathf.Clamp(currectIndex + (int)Input.mouseScrollDelta.y, 0, BuyCosts.Length - 1);
        currentType = (BuyType)Mathf.Clamp(currectIndex + (int)Input.mouseScrollDelta.y, 0, BuyCosts.Length - 1);
        cost_txt.text = BuyCosts[currectIndex].ToString();
        upgratePanel[currectIndex].SetActive(true);
    }

    private void TurelType()
    {
        if(turelNumber + 1 == turelPoint.Length)
            return;
        SetTurel();
        ResourceColection.instance.RemoveResource(BuyCosts[(int)BuyType.Turel]);
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