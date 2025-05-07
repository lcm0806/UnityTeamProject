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
    public Camera renderCamera;

    private int currentIndex = 0;
    private bool isGameActive = false;
    private bool isPaused = false;
    private Vector2 targetPosition;

    public GameObject player;

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

        if (renderCamera != null)
            renderCamera.enabled = false; // 게임 시작 전에 카메라 꺼둠

        if (gameUIWrapper != null)
            gameUIWrapper.SetActive(false);

        if (settingPanel != null)
            settingPanel.SetActive(false);

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
        GameBootFlags.isNewGame = true;
        SceneManager.LoadScene("GameScene");
    }

    public void NewGame()
    {
        GameBootFlags.isNewGame = true;
        SceneManager.LoadScene("GameScene");
    }

    public void OnContinueButton()
    {
        if (!isGameActive)
        {
            Debug.Log("Continue Game Start!");
            StartGame(false); // 초기화 OFF
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

    private void StartGame(bool resetPlayer)
    {
        isGameActive = true;
        isPaused = false;
        Time.timeScale = 1f;

        if (panelParent != null)
            panelParent.gameObject.SetActive(false);

        if (settingPanel != null)
            settingPanel.SetActive(false);

        if (renderCamera != null)
            renderCamera.enabled = true;

        if (gameUIWrapper != null)
        {
            gameUIWrapper.SetActive(true);

            Canvas canvas = gameUIWrapper.GetComponent<Canvas>();
            if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                if (renderCamera != null)
                    canvas.worldCamera = renderCamera;
                else
                    Debug.LogWarning("RenderCamera가 연결되지 않았습니다.");
            }
        }


        if (player != null)
        {
            player.SetActive(true);
            Player playerScript = player.GetComponent<Player>();
            //playerScript.isActive = true;

            if (resetPlayer) // 초기화 여부에 따라 분기
            {
                playerScript.CulHealth = playerScript.MaxHealth;
                player.transform.position = new Vector3(0f, 5f, 0f);
                player.transform.rotation = Quaternion.identity;
            }
        }

        if (resetPlayer && player != null)
        {
            var playerScript = player.GetComponent<Player>();
            playerScript.ResetPlayer();

            FindObjectOfType<PassiveEquipmentUI>()?.ResetUI();
            FindObjectOfType<ActiveEquipmentUI>()?.ResetUI();
        }

    }




    private void PauseGame()
    {
        if (isGameActive && !isPaused)
        {
            isGameActive = false;
            isPaused = false;

            if (gameUIWrapper != null)
                gameUIWrapper.SetActive(false);

            if (panelParent != null)
                panelParent.gameObject.SetActive(true);


            if (panels.Length > 2 && panels[2] != null)
                panels[2].SetActive(true);

            currentIndex = 2;
            UpdatePanels();


            if (renderCamera != null)
                renderCamera.enabled = false;

            Camera mainCam = GameObject.Find("Main Camera")?.GetComponent<Camera>();
            if (mainCam != null)
                mainCam.enabled = true;

            Debug.Log("ESC → Opening 화면으로 복귀");
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
