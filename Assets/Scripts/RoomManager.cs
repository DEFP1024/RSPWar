using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI playerOneName;
    [SerializeField] TextMeshProUGUI playerTwoName;

    [SerializeField] Button roomButton;

    bool playerOnealready = false;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => PhotonNetwork.InRoom);
        Player[] players = PhotonNetwork.PlayerList;

        foreach (var p in players)
        {
            if(playerOnealready == false)
            {
                playerOneName.text = p.NickName;
                playerOnealready = true;
            }

            else
            {
                playerTwoName.text = p.NickName;
            }
        }

        if(PhotonNetwork.IsMasterClient == false)
        {
            roomButton.interactable = false;
        }
    }

    public void StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Prototype");
        }
    }

    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playerTwoName.text = newPlayer.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerTwoName.text = "";
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneChanger.LoadScene("Lobby");

    }
}
