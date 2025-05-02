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
    public float panelHeight = 1080f;  // 한 패널의 높이
    public float moveSpeed = 5f;        // 부드럽게 이동하는 속도
    public GameObject settingPanel;
    public GameObject gameUIWrapper;
    public Slider soundSlider;
    public TextMeshProUGUI percentDisplay;
    public RectTransform panelParent; // AllPanelWrapper를 연결할 것

    private int currentIndex = 0;
    private bool isGameActive = false;
    private bool isPaused = false;
    private Vector2 targetPosition;

    void Start()
    {
        if (panelParent == null)
        {
            Debug.LogError("panelParent (AllPanelWrapper) 가 연결되지 않았습니다!");
            return;
        }

        isGameActive = false;
        isPaused = false;
        Time.timeScale = 1f;

        if (gameUIWrapper != null) gameUIWrapper.SetActive(false);
        if (settingPanel != null) settingPanel.SetActive(false);

        targetPosition = panelParent.anchoredPosition;
        UpdatePanels();

        if (soundSlider != null)
            soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);

        if (soundSlider != null)
            OnSoundSliderChanged(soundSlider.value);
    }

    void Update()
    {
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

        if (panelParent != null)
        {
            panelParent.anchoredPosition = Vector2.Lerp(panelParent.anchoredPosition, targetPosition, Time.unscaledDeltaTime * moveSpeed);
        }
    }

    void UpdatePanels()
    {
        if (panelParent != null)
        {
            targetPosition = new Vector2(0, currentIndex * panelHeight);
        }

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
        //if (panelParent != null)
        //    panelParent.gameObject.SetActive(false);

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

        // if (panelParent != null)
        //     panelParent.gameObject.SetActive(true);

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

        if (panelParent != null)
            panelParent.gameObject.SetActive(false);

        if (settingPanel != null)
            settingPanel.SetActive(false);

        if (gameUIWrapper != null)
            gameUIWrapper.SetActive(true);
    }

    private void PauseGame()
    {
        if (gameUIWrapper != null && gameUIWrapper.activeSelf)
        {
            // HealthUI가 켜져있으면 HealthUI를 끄고 Menu로 돌아간다
            gameUIWrapper.SetActive(false);

            if (panelParent != null)
                panelParent.gameObject.SetActive(true);

            currentIndex = 2; // MenuPanel 인덱스
            UpdatePanels();

            isGameActive = false; // ★ 추가: 게임 비활성화 상태로 바꿔야 한다
            isPaused = false; // ★ 추가: 일시정지도 아니다
        }
        else
        {
            // 일반 Pause (게임이 진행중일 때 ESC)
            isPaused = true;

            if (panelParent != null)
                panelParent.gameObject.SetActive(false);

            if (panels.Length > 2)
                panels[2].SetActive(true);

            if (gameUIWrapper != null)
                gameUIWrapper.SetActive(false);
        }
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (panels.Length > 2)
            panels[2].SetActive(false);

        if (panelParent != null)
            panelParent.gameObject.SetActive(true);

        if (gameUIWrapper != null)
            gameUIWrapper.SetActive(true);
    }

    private void OnSoundSliderChanged(float value)
    {
        if (percentDisplay != null && soundSlider != null)
        {
            float normalized = value / soundSlider.maxValue;
            int pct = Mathf.RoundToInt(normalized * 100f);
            percentDisplay.text = pct + "%";
        }
    }

    public void OnGoBackButton()
    {
        Debug.Log("Go back to Menu Panel");

        if (settingPanel != null)
            settingPanel.SetActive(false);

        if (panelParent != null)
            panelParent.gameObject.SetActive(true);

        currentIndex = 2;
        UpdatePanels();
    }
}
