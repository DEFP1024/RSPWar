using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField createRoomName;
    [SerializeField] private TextMeshProUGUI createRoomNameWord;

    [SerializeField] private TMP_InputField JoinRoomName;
    [SerializeField] private TextMeshProUGUI JoinRoomNameWord;

    [SerializeField] private TMP_InputField playerNickName;
    [SerializeField] private TextMeshProUGUI playerNickNameWord;

    [SerializeField] private int maxNickWordLength;
    [SerializeField] private int maxRoomWordLength;



    private void Start()
    {
        string countRoomText = $"[Input Word Length] / {maxRoomWordLength}";
        string countNickText = $"[Input Word Length] / {maxNickWordLength}";

        playerNickName.characterLimit = maxNickWordLength;
        playerNickName.onValueChanged.AddListener((word) => playerNickName.text = Regex.Replace(word, @"[^0-9a-zA-Z가-힣ㄱ-ㅎ]", ""));
        playerNickNameWord.text = countNickText.Replace("[Input Word Length]", $"{playerNickName.text.Length}");
        playerNickName.onValueChanged.AddListener(_ => playerNickNameWord.text = countNickText.Replace("[Input Word Length]", $"{playerNickName.text.Length}"));

        createRoomName.characterLimit = maxRoomWordLength;
        createRoomName.onValueChanged.AddListener((word) => createRoomName.text = Regex.Replace(word, @"[^0-9a-zA-Z가-힣ㄱ-ㅎ ]", ""));
        createRoomNameWord.text = countRoomText.Replace("[Input Word Length]", $"{createRoomName.text.Length}");
        createRoomName.onValueChanged.AddListener(_ => createRoomNameWord.text = countRoomText.Replace("[Input Word Length]", $"{createRoomName.text.Length}"));

        JoinRoomName.characterLimit = maxRoomWordLength;
        JoinRoomName.onValueChanged.AddListener((word) => JoinRoomName.text = Regex.Replace(word, @"[^0-9a-zA-Z가-힣ㄱ-ㅎ ]", ""));
        JoinRoomNameWord.text = countRoomText.Replace("[Input Word Length]", $"{JoinRoomName.text.Length}");
        JoinRoomName.onValueChanged.AddListener(_ => JoinRoomNameWord.text = countRoomText.Replace("[Input Word Length]", $"{JoinRoomName.text.Length}"));
    }
}
