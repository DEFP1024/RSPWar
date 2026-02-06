using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private Button rockButton;
    [SerializeField] private Button scissorsButton;
    [SerializeField] private Button paperButton;

    void Start()
    {
        rockButton.onClick.AddListener(() => SelectorManager.Instance.SelectCharacter(Character.Rock));
        scissorsButton.onClick.AddListener(() => SelectorManager.Instance.SelectCharacter(Character.Scissors));
        paperButton.onClick.AddListener(() => SelectorManager.Instance.SelectCharacter(Character.Paper));
    }
}
