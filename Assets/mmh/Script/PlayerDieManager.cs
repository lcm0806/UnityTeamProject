using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDieManager : MonoBehaviour
{
    public GameObject deathPanel;       // You Died 패널
    public Button restartButton;        // 재시작 버튼
    public Button quitButton;           // 종료 버튼
    public string sceneName = "GameScene"; // 현재 씬 이름 (필요시 설정)

    private void Start()
    {
        // 시작 시 DeathPanel 끄기
        deathPanel.SetActive(false);

        // 버튼에 기능 연결
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowDeathScreen()
    {
        // 사망 시 UI 띄우기
        deathPanel.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(sceneName); // 현재 씬 다시 로드
    }

    private void QuitGame()
    {
        Application.Quit(); // 종료 (에디터에선 안됨)
    }
}
