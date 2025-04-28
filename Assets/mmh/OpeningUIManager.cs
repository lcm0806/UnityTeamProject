using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningUIManager : MonoBehaviour
{
    public GameObject[] panels; // TitlePanel, SaveSlotsPanel, MenuPanel 순서
    private int currentIndex = 0;

    public GameObject healthUI; // HealthUI 연결

    void Start()
    {
        // 게임 시작할 때 HealthUI는 꺼진 상태
        if (healthUI != null)
            healthUI.SetActive(false);

        // 패널 초기화 (TitlePanel만 켜고 나머지 끔)
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0); // 0번 인덱스만 true (TitlePanel)
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;
            if (currentIndex >= panels.Length)
                currentIndex = 0;
            UpdatePanels();
        }
    }

    void UpdatePanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentIndex);
        }
    }

    public void OnNewGameButton()
    {
        Debug.Log("New Game Start!");
        StartGame();
    }

    public void OnContinueButton()
    {
        Debug.Log("Continue Game!");
        StartGame();
    }

    private void StartGame()
    {
        // 모든 오프닝 패널 끄기
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // HealthUI 켜기
        if (healthUI != null)
            healthUI.SetActive(true);

        // 여기서 나중에 "게임플레이 씬 이동"도 추가 가능
        // SceneManager.LoadScene("게임플레이씬이름");
    }
}