using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public Transform player;
    public int playerHealth = 3; // 예시: HealthManager 연결도 가능
    public int itemCount = 0;    // 예시: 보유 아이템 수 등

    private string GetKey(string baseKey, int slot)
    {
        return $"{baseKey}_Slot{slot}";
    }

    public void SaveGame(int slot)
    {
        PlayerPrefs.SetFloat(GetKey("PlayerPosX", slot), player.position.x);
        PlayerPrefs.SetFloat(GetKey("PlayerPosY", slot), player.position.y);
        PlayerPrefs.SetFloat(GetKey("PlayerPosZ", slot), player.position.z);

        PlayerPrefs.SetInt(GetKey("Health", slot), playerHealth);
        PlayerPrefs.SetInt(GetKey("ItemCount", slot), itemCount);

        PlayerPrefs.Save();
        Debug.Log($"게임이 슬롯 {slot}에 저장되었습니다.");
    }

    public void LoadGame(int slot)
    {
        if (!PlayerPrefs.HasKey(GetKey("PlayerPosX", slot)))
        {
            Debug.LogWarning("해당 슬롯에 저장된 데이터가 없습니다.");
            return;
        }

        Vector3 pos = new Vector3(
            PlayerPrefs.GetFloat(GetKey("PlayerPosX", slot)),
            PlayerPrefs.GetFloat(GetKey("PlayerPosY", slot)),
            PlayerPrefs.GetFloat(GetKey("PlayerPosZ", slot))
        );

        player.position = pos;

        playerHealth = PlayerPrefs.GetInt(GetKey("Health", slot));
        itemCount = PlayerPrefs.GetInt(GetKey("ItemCount", slot));

        Debug.Log($"게임이 슬롯 {slot}에서 불러와졌습니다.");
    }

    public void DeleteGame(int slot)
    {
        PlayerPrefs.DeleteKey(GetKey("PlayerPosX", slot));
        PlayerPrefs.DeleteKey(GetKey("PlayerPosY", slot));
        PlayerPrefs.DeleteKey(GetKey("PlayerPosZ", slot));
        PlayerPrefs.DeleteKey(GetKey("Health", slot));
        PlayerPrefs.DeleteKey(GetKey("ItemCount", slot));

        Debug.Log($"슬롯 {slot}의 저장 데이터를 삭제했습니다.");
    }
}