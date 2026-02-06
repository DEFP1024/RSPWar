using System;
using UnityEngine;

public class CharacterSelecter : MonoBehaviour,ICharacter
{
    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject scissorsPrefab;
    [SerializeField] GameObject paperPrefab;

    public Character Character { get; set; }

    public event Action OnChangeCharacter;

    public void ChangeCharacter(Character character)
    {
        Character = character;
        Debug.Log(Character);
        EventManager.Instance.ChangeCharacter();
    }
}
