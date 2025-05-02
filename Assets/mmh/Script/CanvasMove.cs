using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMove : MonoBehaviour
{
    public float moveDistance = 1080f;  // 한 번 이동할 거리
    public float moveSpeed = 500f;         // 이동 속도

    private Vector3 targetPosition;     // 목표 위치
    private bool isMoving = false;       // 이동 중 여부

    void Start()
    {
        targetPosition = transform.position;  // 시작 위치 저장
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving)
        {
            targetPosition = transform.position + new Vector3(0, -moveDistance, 0);
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}