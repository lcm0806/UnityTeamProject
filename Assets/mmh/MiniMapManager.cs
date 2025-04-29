using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    public Image[,] roomImages = new Image[4, 4]; // 3x3 방
    public Color normalColor = Color.gray;
    public Color currentColor = Color.yellow;

    private Vector2Int currentPos = new Vector2Int(1, 1); // 초기 위치 (중앙)

    void Start()
    {
        // roomImages 배열 자동으로 채우기
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                string name = $"Room_{y}_{x}";
                Transform t = transform.Find(name);
                if (t != null)
                {
                    roomImages[y, x] = t.GetComponent<Image>();
                    roomImages[y, x].color = normalColor;
                }
            }
        }

        UpdateMiniMap();
    }

    public void MoveTo(int x, int y)
    {
        if (x < 0 || x >= 4 || y < 0 || y >= 4) return;

        currentPos = new Vector2Int(y, x); // (행, 열 순서)
        UpdateMiniMap();
    }

    void UpdateMiniMap()
    {
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (roomImages[y, x] != null)
                    roomImages[y, x].color = (currentPos.x == y && currentPos.y == x) ? currentColor : normalColor;
            }
        }
    }
}