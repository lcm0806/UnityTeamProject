using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts; // ��Ʈ �̹��� �迭
    public Sprite fullHeart; // ���� ��Ʈ
    public Sprite emptyHeart; // �� ��Ʈ
    private Player playerInstance;
    private int previousHealth;

    private void Awake()
    {
        playerInstance = Player.Instance;
        if (playerInstance == null)
        {
            Debug.LogError("Player �ν��Ͻ��� ã�� �� �����ϴ�. HealthManager�� ����� �۵����� ���� �� �ֽ��ϴ�.");
            enabled = false;
            return;
        }

        // ��Ʈ �迭 �ʱ�ȭ Ȯ��
        if (hearts == null || hearts.Length == 0)
        {
            Debug.LogError("��Ʈ �̹��� �迭�� �ʱ�ȭ���� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }

        previousHealth = playerInstance.CulHealth; // �ʱ� ü�� ���� ����
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

        // ��Ʈ ������ �ִ� ü�º��� ������ ����
        if (hearts.Length > maxHealth)
        {
            Debug.LogWarning("��Ʈ UI ��Ұ� �ִ� ü�º��� �����ϴ�. UI ������ Ȯ���ϼ���.");
            for (int i = maxHealth; i < hearts.Length; i++)
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
        else if (hearts.Length < maxHealth)
        {
            Debug.LogWarning("��Ʈ UI ��Ұ� �ִ� ü�º��� �����ϴ�. ��� ü���� ǥ������ ���� �� �ֽ��ϴ�.");
            // �ʿ��ϴٸ� �������� ��Ʈ UI�� �߰��ϴ� ���� ���� ����
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].gameObject.SetActive(true); // Ȱ��ȭ
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                if (i < maxHealth) // �ִ� ü�� ���� ���� �� ��Ʈ�� Ȱ��ȭ
                {
                    hearts[i].gameObject.SetActive(true);
                }
            }
        }
    }

    public void ResetHealth(int max)
    {
      //this.health = max;
      //this.maxHealth = max;
      //UpdateHearts();
    }
}