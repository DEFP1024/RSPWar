using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    int playerHp = 100;

    public void Damage(int damage)
    {
        playerHp -= damage;
        if (playerHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
     
    }
}
