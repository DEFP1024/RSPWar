using System;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public event Action OnChangeCharacter;


    public void ChangeCharacter()
    {
        OnChangeCharacter?.Invoke();
    }
}
