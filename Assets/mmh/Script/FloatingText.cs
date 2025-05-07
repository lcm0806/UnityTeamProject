using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float duration = 2f;
    public float floatSpeed = 1f;
    private TextMeshProUGUI text;

    void Awake()
    {
        // 자식 오브젝트 포함해서 TextMeshProUGUI 컴포넌트 찾기
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string message)
    {
        if (text != null)
        {
            text.SetText(message);
        }
        else
        {
            Debug.LogWarning("FloatingText: TextMeshProUGUI가 연결되지 않았습니다.");
        }
    }

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
}
