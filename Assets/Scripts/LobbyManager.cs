using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField createRoomInput;
    [SerializeField] TMP_InputField joinRoomInput;
    [SerializeField] TMP_InputField nickNameInput;

    [SerializeField] GameObject roomPrefab;

    [SerializeField] Transform roomListPanel;

    [SerializeField] TextMeshProUGUI infoText;

    private readonly Dictionary<string, GameObject> roomByName = new();

    private bool cursorLocked = false;

    private void Start()
    {
        cursorLocked = false;
        if (RoomListCache.Instance != null)
        {
            FirstRoomListUpdate(RoomListCache.Instance.Rooms.Values);
        }
    }

    private bool NickNameSetting()
    {
        if (NickNameChecker() == false)
        {
            return false;
        }

        PhotonNetwork.NickName = nickNameInput.text;

        return true;
    }

    public void CreateRoom()
    {
        if (NickNameSetting() == false) return;

        PhotonNetwork.CreateRoom(createRoomInput.text, new RoomOptions
        {
            MaxPlayers = 2,
            CustomRoomProperties = new Hashtable()
            {
                { "hostNick", nickNameInput.text}
            }
            ,
            CustomRoomPropertiesForLobby = new[] { "hostNick" }
        });
    }

    public void JoinRoom()
    {
        if (NickNameSetting() == false) return;

        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    public void ButtonJoinRoom(string roomName)
    {
        if (NickNameSetting() == false) return;

        PhotonNetwork.JoinRoom(roomName);
    }

    public void JoinRandomRoom()
    {
        if (NickNameSetting() == false) return;

        PhotonNetwork.JoinRandomOrCreateRoom(null,0,MatchmakingMode.FillRoom,null,null,$"{nickNameInput.text}의 방", new RoomOptions {
            MaxPlayers = 2,
            CustomRoomProperties = new Hashtable()
            {
                { "hostNick", nickNameInput.text}
            }
            ,
            CustomRoomPropertiesForLobby = new[] { "hostNick" }
        });
    }

    public void ExitLobby()
    {
        PhotonNetwork.LeaveLobby();
        SceneChanger.LoadScene("Title");
    }

    public override void OnJoinedRoom()
    {
        SceneChanger.LoadScene("RoomScene");
    }

    private void FirstRoomListUpdate(IEnumerable<RoomInfo> rooms)
    {
        foreach (var room in roomByName)
        {
            Destroy(room.Value);
        }
        roomByName.Clear();

        foreach (var roomInfo in rooms)
        {
            var roomCreate = Instantiate(roomPrefab, roomListPanel);
            roomByName.Add(roomInfo.Name, roomCreate);

            var roomUIButton = roomCreate.GetComponent<Button>();
            string roomName = roomInfo.Name;
            roomUIButton.onClick.AddListener(() => ButtonJoinRoom(roomName));

            var roomButton = roomCreate.GetComponentInChildren<RoomButton>();

            roomButton.playerCountInfoText.text = roomButton.playerCountInfo.Replace("[CurrentPlayer]", roomInfo.PlayerCount.ToString());

            roomButton.roomNameText.text = roomInfo.Name;

            if (roomInfo.CustomProperties.TryGetValue("hostNick", out var hostNick))
            {
                roomButton.nickNameText.text = roomButton.nickName.Replace("[NickName]", hostNick.ToString());
            }

            else { roomButton.nickNameText.text = "알 수 없음";}
        }

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                if(roomByName.TryGetValue(roomInfo.Name, out var oldRoom))
                {
                    Destroy(oldRoom);
                    roomByName.Remove(roomInfo.Name);
                }
                continue;
            }

            if (roomByName.TryGetValue(roomInfo.Name, out var roomCreate) == false)
            {
                roomCreate = Instantiate(roomPrefab, roomListPanel);
                var roomUIButton = roomCreate.GetComponent<Button>();
                roomUIButton.onClick.AddListener(() => ButtonJoinRoom(roomInfo.Name));
                roomByName.Add(roomInfo.Name, roomCreate);
            }

            var roomButton = roomCreate.GetComponentInChildren<RoomButton>();

            roomButton.playerCountInfoText.text = roomButton.playerCountInfo.Replace("[CurrentPlayer]", $"{roomInfo.PlayerCount}");

            roomButton.roomNameText.text = roomInfo.Name;

            if (roomInfo.CustomProperties.TryGetValue("hostNick", out var hostNick))
            {
                roomButton.nickNameText.text = roomButton.nickName.Replace("[NickName]", hostNick.ToString());
            }

            else { roomButton.nickNameText.text = "알 수 없음";}
        }
    }

    private bool NickNameChecker()
    {
        if (nickNameInput.text.Length > 0)
        {
            infoText.text = "";
            return true;
        }

        infoText.text = "닉네임을 입력해주세요";
        return false;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
