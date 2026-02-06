using TMPro;
using UnityEngine;

public class FightUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;

    public string hpTextString;

    private void Start()
    {
        hpTextString = "HP : [CurrentHP]";
    }
}
