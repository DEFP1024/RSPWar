using UnityEngine;
using UnityEngine.UI;

public class SelectorManager : Singleton<SelectorManager>
{
    private CharacterSelecter characterSelecter;
    protected override void Awake()
    {
        base.Awake();
        characterSelecter = GetComponent<CharacterSelecter>();
    }

    public void SelectCharacter(Character character) => characterSelecter.ChangeCharacter(character);

}
