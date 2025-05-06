using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float duration = 1f;

    private Vector3 direction = Vector3.up;
    private float timer = 0f;

    void Update()
    {
        transform.position += direction * floatSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string message, Color color)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = message;
        GetComponentInChildren<TextMeshProUGUI>().color = color;
    }
}
