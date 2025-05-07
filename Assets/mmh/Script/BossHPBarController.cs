using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHPBarController : MonoBehaviour
{
    public Image bossHPFill;
    private float maxHP = 500f;
    private float currentHP;
    private float displayHP;
    public float smoothSpeed = 5f;

    void Start()
    {
        // Level8이 아닐 경우, 체력바 비활성화
        if (SceneManager.GetActiveScene().name != "Level8")
        {
            gameObject.SetActive(false); // BossHPBar 오브젝트 비활성화
            return;
        }

        currentHP = maxHP;
        displayHP = maxHP;
        UpdateUI();
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

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