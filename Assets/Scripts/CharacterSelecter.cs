using UnityEngine;


public class CharacterSelecter : MonoBehaviour,ICharacter
{
    [SerializeField] GameObject player;

    public Character Character { get; set; }

    public void SelectCharacter(Character character)
    {
        Character = character;
    }
}
