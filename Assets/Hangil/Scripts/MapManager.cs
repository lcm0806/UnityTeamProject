using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;           // 싱글톤 인스턴스
    public static int currentMap;                // 현재 맵 번호 (1부터 시작)

    [SerializeField] Camera mainCamera;          // 이동시킬 카메라
    public List<GameObject> Maps;                // 맵 프리팹 리스트
    public int startMapPos;                      // 시작 맵 번호

    [SerializeField] List<Transform> cameraPos;  // 맵별 카메라 위치 리스트

    private Coroutine cameraMoveCoroutine;       // 카메라 이동 코루틴 참조용

    void Awake()
    {
        // 싱글톤 패턴
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // mainCamera 자동 할당
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // 맵 초기화
        currentMap = startMapPos;

        // 초기엔 모든 맵을 켜두되, Start()에서 카메라만 해당 맵으로 이동
        foreach (var map in Maps)
        {
            if (!map.activeSelf)
                map.SetActive(true);
        }
    }

    void Start()
    {
        MoveCamera(); // 시작 시 카메라 이동
    }

    // 부드럽게 카메라를 이동시키는 코루틴
    private IEnumerator MoveCameraSmooth(Vector3 targetPosition, float duration = 1f)
    {
        Vector3 startPos = mainCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }

        mainCamera.transform.position = targetPosition; // 정확한 위치 고정
    }

    // 카메라를 현재 맵 위치로 부드럽게 이동
    public void MoveCamera()
    {
        if (mainCamera != null && currentMap - 1 >= 0 && currentMap - 1 < cameraPos.Count)
        {
            Vector3 targetPos = cameraPos[currentMap - 1].position;

            // 기존 이동 중단
            if (cameraMoveCoroutine != null)
            {
                StopCoroutine(cameraMoveCoroutine);
            }

            // 새 이동 시작
            cameraMoveCoroutine = StartCoroutine(MoveCameraSmooth(targetPos, 1f)); // duration 조절 가능
        }
        else
        {
            Debug.LogWarning("카메라 이동 실패: mainCamera 또는 cameraPos가 올바르지 않습니다.");
        }
    }

    // 맵 전환 + 카메라 이동 처리 (이제 기존 맵을 비활성화하지 않음!)
    public void ChangeMap(int newMapIndex)
    {
        if (newMapIndex < 1 || newMapIndex > Maps.Count)
        {
            Debug.LogWarning($"유효하지 않은 맵 번호: {newMapIndex}");
            return;
        }

        currentMap = newMapIndex;

        // 해당 맵이 꺼져있다면 활성화만 해준다
        if (!Maps[currentMap - 1].activeSelf)
        {
            Maps[currentMap - 1].SetActive(true);
        }

        // 카메라 이동
        MoveCamera();
    }
}