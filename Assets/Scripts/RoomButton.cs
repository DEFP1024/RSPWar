using TMPro;
using UnityEngine;

public class RoomButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI roomNameText;
    [SerializeField] public TextMeshProUGUI nickNameText;
    [SerializeField] public TextMeshProUGUI playerCountInfoText;

    public string nickName;
    public string playerCountInfo;

    private void Awake()
    {
        nickName = "닉네임 : [NickName]";
        playerCountInfo = "[CurrentPlayer] / 2";
    }
}
