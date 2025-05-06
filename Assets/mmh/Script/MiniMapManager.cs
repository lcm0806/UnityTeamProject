using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    [System.Serializable]
    public class RoomData
    {
        public Image roomIcon;
        public bool isVisited;
        public bool isBossRoom;
    }

    public RoomData[] rooms; // 0 = Room1, 1 = Room2, ...
    public int currentRoomIndex = 0;

    public Sprite defaultIcon;
    public Sprite visitedIcon;
    public Sprite currentIcon;
    public Sprite bossIcon;

    void Start()
    {
        UpdateMiniMap();
    }

    public void EnterRoom(int roomIndex)
    {
        rooms[currentRoomIndex].isVisited = true;
        currentRoomIndex = roomIndex;
        UpdateMiniMap();
    }

    void UpdateMiniMap()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i == currentRoomIndex)
            {
                rooms[i].roomIcon.sprite = currentIcon;
            }
            else if (rooms[i].isBossRoom)
            {
                rooms[i].roomIcon.sprite = bossIcon;
            }
            else if (rooms[i].isVisited)
            {
                rooms[i].roomIcon.sprite = visitedIcon;
            }
            else
            {
                rooms[i].roomIcon.sprite = defaultIcon;
            }
        }
    }
}