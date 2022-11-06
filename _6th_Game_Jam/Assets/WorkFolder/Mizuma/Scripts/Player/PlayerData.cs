using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int playerHp = 0;
    public int PlayerHp => playerHp;

    public void HealHp(int num)
    {
        playerHp += num;
    }

    public bool TakeDamage(int num)
    {
        playerHp -= num;
        return playerHp <= 0;
    }
}
