using UnityEngine;

public enum Character
{
    Rock,
    Scissors,
    Paper
}

public interface ICharacter
{
    public Character Character { get; set; }
}
