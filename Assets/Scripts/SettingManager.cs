using TMPro;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [SerializeField] TextMeshProUGUI c;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.GameSet(a, b, c);
        GameManager.instance.GameStart();
    }
}   
