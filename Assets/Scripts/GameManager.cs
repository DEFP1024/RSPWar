using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public Transform spawnPointOne;
    public Transform spawnPointTwo;

    [SerializeField] GameObject playerPrefab;

    [SerializeField] TextMeshProUGUI playerHPUI;

    FirstPersonPlayerController playerController;

    string hpTextString;

    bool playerOnealready = false;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {

        
    }

    public void GameSet(Transform a, Transform b, TextMeshProUGUI c)
    {
        spawnPointOne = a;
        spawnPointTwo = b;
        playerHPUI = c;
        playerController = FindAnyObjectByType<FirstPersonPlayerController>();
    }

    public void GameStart()
    {
        hpTextString = "HP : [CurrentHP]";

        UpdateHp(100);

        Vector3 onePos = spawnPointOne.position;
        Vector3 twoPos = spawnPointTwo.position;

        if (PlayerManager.LocalPlayerInstance == null)
        {

            if (PhotonNetwork.IsMasterClient)
            {
                PlayerManager.LocalPlayerInstance = PhotonNetwork.Instantiate(playerPrefab.name, onePos, Quaternion.identity);
            }
            else
            {
                PlayerManager.LocalPlayerInstance = PhotonNetwork.Instantiate(playerPrefab.name, twoPos, Quaternion.identity);
            }
        }
    }

    public void UpdateHp(int num)
    {
        if (num < 0)
        {
            num = 0;
        }

        playerHPUI.text = hpTextString.Replace("[CurrentHP]", num.ToString());
    }

    public override void OnLeftRoom()
    {
        SceneChanger.LoadScene("Lobby");
    }

    public void LeaveRoom()
    {
        if (playerController)
        {
            playerController.cursorLocked = false;
            playerController.cursorInputForLook = false;
        }
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }

}
