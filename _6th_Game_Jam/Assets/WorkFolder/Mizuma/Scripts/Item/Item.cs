using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType type;

    public TextMeshProUGUI powerText;
    public Rigidbody rb;
    public BoxCollider col;

    private const float delayEachDamage = 12f / 60f;
    private int healPoint;
    private int hp;

    public void Init(int effectPower)
    {
        if(type == ItemType.Enemy || type == ItemType.Enemy_Large)
        {
            UpdateUGUI(effectPower);
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
        Destroy(gameObject, 3f);
        col.enabled = false;
        rb.isKinematic = false;
        rb.AddForce(new Vector3(Random.Range(0.8f, 1.2f), 1f, Random.Range(0.8f, 1.2f)) * 1000f);
        rb.AddTorque(new Vector3(Random.Range(10f, 100f), Random.Range(10f, 100f), Random.Range(10f, 100f)), ForceMode.VelocityChange);
    }

    private IEnumerator DelayTakeDamage(int remHp)
    {
        EffectManager.Instance.PlayEffect(EffectManager.EffectType.Take_Damage, transform.position);
        bool isGameEnd = PlayerManager.Instance.KillChild(1);
        if (isGameEnd == true) yield break;

        remHp--;
        if (remHp <= 0) { UsedItem(); yield break; }

        UpdateUGUI(remHp);
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

    private void UpdateUGUI(int power)
    {
        powerText.text = power.ToString();
    }

    public enum ItemType
    {
        None,
        Heal,
        Enemy,
        Enemy_Large
    }
}
