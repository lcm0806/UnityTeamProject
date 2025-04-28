using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;    // Slider, Text 사용을 위해 추가

public class OpeningUIManager : MonoBehaviour
{
    [Header("◾ Opening panels (0:Title,1:SaveSlots,2:Menu)")]
    public GameObject[] panels;

    [Header("◾ Setting panel (not in 'panels' array!)")]
    public GameObject settingPanel;

    [Header("◾ In‐game HUD")]
    public GameObject healthUI;

    [Header("◾ Sound 설정 (슬라이더 & 퍼센트)")]
    public Slider soundSlider;       // SettingPanel 안의 Slider
    public Text percentDisplay;      // 퍼센트 표시용 Text

    private int currentIndex = 0;
    private bool isGameActive = false;
    private bool isPaused = false;

    void Start()
    {
        // 초기화
        isGameActive = false;
        isPaused = false;
        Time.timeScale = 1f;

        healthUI?.SetActive(false);
        settingPanel?.SetActive(false);
        UpdatePanels();

        // 슬라이더 콜백 연결
        if (soundSlider != null)
            soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
    }

    void Update()
    {
        // ▶ 화살표 네비: 게임 시작 전 & 설정창 닫힌 상태에서만
        if (!isGameActive && (settingPanel == null || !settingPanel.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && currentIndex < panels.Length - 1)
            {
                currentIndex++;
                UpdatePanels();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && currentIndex > 0)
            {
                currentIndex--;
                UpdatePanels();
            }
        }
        // ▶ 게임 중 ESC → 일시정지
        else if (isGameActive && !isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    // panels 중 currentIndex 하나만 켜고, settingPanel은 항상 끔
    void UpdatePanels()
    {
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(i == currentIndex);

        settingPanel?.SetActive(false);
    }

    // --- 버튼 콜백들 ---

    public void OnNewGameButton()
    {
        Debug.Log("New Game Start!");
        StartGame();
    }

    public void OnContinueButton()
    {
        if (!isGameActive)
        {
            Debug.Log("Continue Game Start!");
            StartGame();
        }
        else if (isPaused)
        {
            Debug.Log("Resume Game!");
            ResumeGame();
        }
    }

    public void OnSettingButton()
    {
        Debug.Log("Go to Setting Panel");
        // 모든 오프닝 메뉴 숨기기
        foreach (var p in panels) p.SetActive(false);

        // 설정창만 켜고 최상위로 이동
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
            settingPanel.transform.SetAsLastSibling();
        }
    }

    public void OnBackFromSetting()
    {
        Debug.Log("Back from Setting");
        settingPanel?.SetActive(false);
        currentIndex = 2; // MenuPanel 인덱스
        UpdatePanels();
    }

    public void OnExitButton()
    {
        Debug.Log("Exit Game");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // --- 게임 시작 / 일시정지 / 재개 ---

    private void StartGame()
    {
        isGameActive = true;
        isPaused = false;
        Time.timeScale = 1f;

        foreach (var p in panels) p.SetActive(false);
        settingPanel?.SetActive(false);
        healthUI?.SetActive(true);
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        foreach (var p in panels) p.SetActive(false);
        panels[2].SetActive(true); // MenuPanel
        healthUI?.SetActive(false);
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        panels[2].SetActive(false);
        healthUI?.SetActive(true);
    }

    // --- 슬라이더 값 변경 콜백 ---
    private void OnSoundSliderChanged(float value)
    {
        if (percentDisplay != null && soundSlider != null)
        {
            float normalized = value / soundSlider.maxValue;
            int pct = Mathf.RoundToInt(normalized * 100f);
            percentDisplay.text = pct + "%";

            // 실제 볼륨 적용 (원한다면)
            // AudioListener.volume = normalized;
        }
    }
}