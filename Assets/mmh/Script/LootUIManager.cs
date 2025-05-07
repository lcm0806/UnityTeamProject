using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LootUIManager : MonoBehaviour
{
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI keyText;

    private int bombCount = 0;
    private int keyCount = 0;

    public void AddBomb()
    {
        bombCount++;
        UpdateUI();
    }

    public void AddKey()
    {
        keyCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        bombText.text = bombCount.ToString();
        keyText.text = keyCount.ToString();
    }

    public void ResetCounts()
    {
        bombCount = 0;
        keyCount = 0;
        UpdateUI();
    }
}