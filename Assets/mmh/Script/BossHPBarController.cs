using UnityEngine;
using UnityEngine.UI;

public class BossHPBarController : MonoBehaviour
{
    public Image bossHPFill;
    private float maxHP = 500f;
    private float currentHP;
    private float displayHP;
    public float smoothSpeed = 5f;

    void Start()
    {
        currentHP = maxHP;
        displayHP = maxHP;
        UpdateUI();
    }

    void Update()
    {
        // 부드러운 UI 반영
        if (displayHP != currentHP)
        {
            displayHP = Mathf.Lerp(displayHP, currentHP, Time.deltaTime * smoothSpeed);
            UpdateUI();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    private void UpdateUI()
    {
        bossHPFill.fillAmount = displayHP / maxHP;
    }
}