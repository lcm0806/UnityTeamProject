using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts; // 하트 이미지 배열
    public Sprite fullHeart; // 빨간 하트
    public Sprite emptyHeart; // 빈 하트
    public int health = 3; // 현재 체력
    private int maxHealth = 3; // 최대 체력

    void Update()
    {
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}