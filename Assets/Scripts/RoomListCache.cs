using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;

public class RoomListCache : MonoBehaviourPunCallbacks
{
    public static RoomListCache Instance { get; private set; }

    private readonly Dictionary<string, RoomInfo> rooms = new();

    public IReadOnlyDictionary<string, RoomInfo> Rooms => rooms;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var room in roomList)
        {
            if (room.RemovedFromList)
            {
                rooms.Remove(room.Name);
                continue;
            }

            rooms[room.Name] = room;
        }
    }
}
