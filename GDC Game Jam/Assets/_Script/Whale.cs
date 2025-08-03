using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Whale : MonoBehaviour
{
    public float Hp;
    public float maxHp;
    public event Action OnGameOver;

    [SerializeField] private Image HpBar;

    private void Start()
    {
        ShowHpBar();
        OnGameOver += GameOverManager.instance.ShowGameOver;
    }

    public void TakeDamage(float damage)
    {
        Hp-=damage;
        ShowHpBar();

        if (Hp <= 0f)
            GameOver();
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();
        return;
    }

    private void ShowHpBar()
    {
        HpBar.fillAmount = Hp / maxHp;
    }
    public void HealWhale(int number)
    {
        Hp = Mathf.Clamp(Hp + number, 0f, maxHp);
        ShowHpBar();
    }
}