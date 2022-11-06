using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType type;

    private const float delayEachDamage = 12f / 60f;

    private int healPoint;
    private int hp;

    private void Start()
    {
        if (type == ItemType.Enemy) Init(0, 1);
        else if (type == ItemType.Enemy_Large) Init(0, 5);
        else if (type == ItemType.Heal) Init(1, 1);
        else Debug.LogError("ItemType = NONE !");

        Debug.Log("Spawn: " + type);
    }

    public void Init(int healPoint, int hp)
    {
        this.healPoint = healPoint;
        this.hp = hp;
    }

    private void PlayItemEffect(Collider other)
    {
        if(type == ItemType.Enemy || type == ItemType.Enemy_Large)
        {
            PlayerManager.Instance.StartStop();
            StartCoroutine(DelayTakeDamage(hp));
        }
        else if(type == ItemType.Heal)
        {
            PlayerManager.Instance.BirthChild(new GameObject[] { this.gameObject });
            Destroy(GetComponent<BoxCollider>());
            //UsedItem();
        }
    }

    private void UsedItem()
    {
        PlayerManager.Instance.StartMove();
        // TODO: 敵死亡エフェクト
        Destroy(this.gameObject);
    }

    private IEnumerator DelayTakeDamage(int remHp)
    {
        Debug.Log("Damage");
        // TODO: 敵ダメージエフェクト
        PlayerManager.Instance.KillChild(1);
        remHp--;
        if (remHp <= 0) UsedItem();

        yield return new WaitForSeconds(delayEachDamage);
        StartCoroutine(DelayTakeDamage(remHp));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == true)
        {
            PlayItemEffect(other);
        }
    }

    public enum ItemType
    {
        None,
        Heal,
        Enemy,
        Enemy_Large
    }
}
