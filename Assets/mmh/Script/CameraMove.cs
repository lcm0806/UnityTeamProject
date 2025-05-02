using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3[] panelPositions;
    public float moveSpeed = 1000f;  // 이동 속도. 필요에 따라 조정 가능
    private Vector3 targetPos;

    void Start()
    {
        if (panelPositions != null && panelPositions.Length > 0)
        {
            targetPos = panelPositions[0];
            transform.position = targetPos;
        }
    }

    void Update()
    {
        if (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    public void MoveToPanel(int index)
    {
        if (panelPositions != null && index >= 0 && index < panelPositions.Length)
        {
            targetPos = panelPositions[index];
        }
    }
}