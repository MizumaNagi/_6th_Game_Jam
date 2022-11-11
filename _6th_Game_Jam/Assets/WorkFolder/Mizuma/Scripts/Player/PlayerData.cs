using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private const int InitPlayerHp = 1;

    private int playerHp = InitPlayerHp;
    private int maxPlayerHp = InitPlayerHp;

    public int PlayerHp => playerHp;
    public int MaxPlayerHp => maxPlayerHp;

    public void HealHp(int num)
    {
        playerHp += num;
        maxPlayerHp = Mathf.Max(playerHp, maxPlayerHp);
    }

    public bool TakeDamage(int num)
    {
        playerHp -= num;
        return playerHp <= 0;
    }
}
