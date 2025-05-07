using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts; // 하트 이미지 배열
    public Sprite fullHeart; // 빨간 하트
    public Sprite emptyHeart; // 빈 하트
    private Player playerInstance;
    private int previousHealth;

    private void Awake()
    {
        playerInstance = Player.Instance;
        if (playerInstance == null)
        {
            Debug.LogError("Player 인스턴스를 찾을 수 없습니다. HealthManager가 제대로 작동하지 않을 수 있습니다.");
            enabled = false;
            return;
        }

        // 하트 배열 초기화 확인
        if (hearts == null || hearts.Length == 0)
        {
            Debug.LogError("하트 이미지 배열이 초기화되지 않았습니다!");
            enabled = false;
            return;
        }

        previousHealth = playerInstance.CulHealth; // 초기 체력 상태 저장
        UpdateHearts();
    }

    void Update()
    {
        if (playerInstance != null && playerInstance.CulHealth != previousHealth)
        {
            previousHealth = playerInstance.CulHealth;
            UpdateHearts();
        }
    }


    void UpdateHearts()
    {
        if (hearts == null || playerInstance == null) return;

        int currentHealth = playerInstance.CulHealth;
        int maxHealth = playerInstance.MaxHealth;

        // 하트 개수가 최대 체력보다 많으면 조정
        if (hearts.Length > maxHealth)
        {
            Debug.LogWarning("하트 UI 요소가 최대 체력보다 많습니다. UI 설정을 확인하세요.");
            for (int i = maxHealth; i < hearts.Length; i++)
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
        else if (hearts.Length < maxHealth)
        {
            Debug.LogWarning("하트 UI 요소가 최대 체력보다 적습니다. 모든 체력을 표시하지 못할 수 있습니다.");
            // 필요하다면 동적으로 하트 UI를 추가하는 로직 구현 가능
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].gameObject.SetActive(true); // 활성화
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                if (i < maxHealth) // 최대 체력 범위 내의 빈 하트는 활성화
                {
                    hearts[i].gameObject.SetActive(true);
                }
            }
        }
    }
}