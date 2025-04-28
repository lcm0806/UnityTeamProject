using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class OpeningUIManager : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject settingPanel;
    public GameObject healthUI;
    public Slider soundSlider;
    public TextMeshProUGUI percentDisplay;


    private int currentIndex = 0;
    private bool isGameActive = false;
    private bool isPaused = false;

    void Start()
    {
        isGameActive = false;
        isPaused = false;
        Time.timeScale = 1f;


        if (healthUI != null) healthUI.SetActive(false);
        if (settingPanel != null) settingPanel.SetActive(false);

        UpdatePanels();

        // 슬라이더 이벤트 연결
        if (soundSlider != null)
            soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);

        // 시작 시 슬라이더 퍼센트 표시도 갱신
        if (soundSlider != null)
            OnSoundSliderChanged(soundSlider.value);

    }

    void Update()
    {

        // 게임이 아직 시작 전이고, 설정창이 꺼진 상태일 때만 방향키 네비게이션 허용

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

        else if (isGameActive && !isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }


    void UpdatePanels()
    {
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(i == currentIndex);


        if (settingPanel != null)
            settingPanel.SetActive(false);
    }

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

        foreach (var p in panels)
            p.SetActive(false);


        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
            settingPanel.transform.SetAsLastSibling();
        }
    }

    public void OnBackFromSetting()
    {
        Debug.Log("Back from Setting");
        if (settingPanel != null)
            settingPanel.SetActive(false);

        currentIndex = 2;

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


    private void StartGame()
    {
        isGameActive = true;
        isPaused = false;
        Time.timeScale = 1f;


        foreach (var p in panels)
            p.SetActive(false);

        if (settingPanel != null)
            settingPanel.SetActive(false);

        if (healthUI != null)
            healthUI.SetActive(true);

    }

    private void PauseGame()
    {
        isPaused = true;


        foreach (var p in panels)
            p.SetActive(false);

        panels[2].SetActive(true);

        if (healthUI != null)
            healthUI.SetActive(false);

    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (panels.Length > 2)
            panels[2].SetActive(false);

        if (healthUI != null)
            healthUI.SetActive(true);
    }


    private void OnSoundSliderChanged(float value)
    {
        if (percentDisplay != null && soundSlider != null)
        {
            float normalized = value / soundSlider.maxValue;
            int pct = Mathf.RoundToInt(normalized * 100f);
            percentDisplay.text = pct + "%";


            // AudioListener.volume = normalized; // 필요하면 사용
        }
    }

    public void OnGoBackButton()
    {
        Debug.Log("Go back to Menu Panel");

        if (settingPanel != null)
            settingPanel.SetActive(false);

        currentIndex = 2; // MenuPanel로 돌아가기 (현재 panels 배열에서 인덱스 2번이 MenuPanel이라 가정)
        UpdatePanels();
    }
}

