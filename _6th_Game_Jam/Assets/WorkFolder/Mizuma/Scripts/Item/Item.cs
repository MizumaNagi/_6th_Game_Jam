using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType type;

    private const float delayEachDamage = 12f / 60f;

    private int healPoint;
    private int hp;

    public void Init(int effectPower)
    {

        if(type == ItemType.Enemy || type == ItemType.Enemy_Large)
        {
            healPoint = 0;
            hp = effectPower;
        }
        else if(type == ItemType.Heal)
        {
            healPoint = effectPower;
            hp = 1;
        }
    }

    private void PlayItemEffect(Collider other)
    {
        if (type == ItemType.Enemy || type == ItemType.Enemy_Large)
        {
            PlayerManager.Instance.StartStop();
            StartCoroutine(DelayTakeDamage(hp));
        }
        else if(type == ItemType.Heal)
        {
            List<GameObject> childs = new List<GameObject>();
            for(int i = 0; i < transform.childCount; i++)
            {
                childs.Add(transform.GetChild(i).gameObject);
            }
            PlayerManager.Instance.BirthChild(childs.ToArray());
            Destroy(GetComponent<BoxCollider>());
            Destroy(gameObject, 0f);
            //UsedItem();
        }
    }

    private void UsedItem()
    {
        PlayerManager.Instance.StartMove();
        EffectManager.Instance.PlayEffect(EffectManager.EffectType.Death_Enemy, transform.position);
        Destroy(gameObject);
    }

    private IEnumerator DelayTakeDamage(int remHp)
    {
        EffectManager.Instance.PlayEffect(EffectManager.EffectType.Take_Damage, transform.position);
        bool isGameEnd = PlayerManager.Instance.KillChild(1);
        if (isGameEnd == true) yield break;

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
