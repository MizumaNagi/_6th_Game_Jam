using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : SingletonClass<ItemFactory>
{
    [SerializeField] private GameObject[] enemyPrefabArr;
    [SerializeField] private GameObject[] allyPrefabArr;
    [SerializeField] private GameObject castlePrefab;

    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform enemyLargeParent;
    [SerializeField] private Transform healParent;

    private void Start()
    {
        // 1�|�C���g�񕜂���A�C�e����(0, 0, 15)�ɐ�������
        ItemFactory.Instance.CreateItem(Item.ItemType.Heal, 1, new Vector3(0, 0, 15));

        // 1�|�C���g��HP�����G��(0, 0, 30)�ɐ�������
        ItemFactory.Instance.CreateItem(Item.ItemType.Enemy, 1, new Vector3(0, 0, 30));

        // 5�|�C���g��HP��������ȓG��(0, 0, 45)�ɐ�������
        ItemFactory.Instance.CreateItem(Item.ItemType.Enemy_Large, 5, new Vector3(0, 0, 45));
    }

    public void CreateItem(Item.ItemType type, int effectPower, Vector3 pos, Vector3? rot = null)
    {
        Vector3 targetRot = rot ?? new Vector3(0f, 180f, 0f);
        Transform newItem = null;
        Item itemCompo = null;

        if (type == Item.ItemType.Enemy)
        {
            int rnd = Random.Range(0, enemyPrefabArr.Length);
            newItem = Instantiate(enemyPrefabArr[rnd], pos, Quaternion.Euler(targetRot)).transform;
            itemCompo = newItem.GetComponent<Item>();
            newItem.SetParent(enemyParent);
        }
        else if(type == Item.ItemType.Enemy_Large)
        {
            newItem = Instantiate(castlePrefab, pos, Quaternion.Euler(targetRot)).transform;
            for(int i = 1; i < 6; i++)
            {
                int rnd = Random.Range(0, enemyPrefabArr.Length);
                Transform newEnemy = Instantiate(enemyPrefabArr[rnd], newItem.GetChild(i).position, newItem.GetChild(i).rotation).transform;
                newEnemy.SetParent(newItem);
                newEnemy.GetComponent<Item>().enabled = false;
                newEnemy.GetComponent<BoxCollider>().enabled = false;
            }
            itemCompo = newItem.GetComponent<Item>();
            newItem.transform.SetParent(enemyLargeParent);
        }
        else if(type == Item.ItemType.Heal)
        {
            int rnd = Random.Range(0, allyPrefabArr.Length);
            newItem = Instantiate(allyPrefabArr[rnd], pos, Quaternion.Euler(targetRot)).transform;
            newItem.SetParent(healParent);

            BoxCollider collider = newItem.gameObject.AddComponent<BoxCollider>();
            collider.center = new Vector3(0.5f, 7.5f, 0f);
            collider.size = new Vector3(10f, 15f, 5f);
            collider.isTrigger = true;

            itemCompo = newItem.gameObject.AddComponent<Item>();
            itemCompo.type = Item.ItemType.Heal;
        }

        itemCompo.Init(effectPower);
    }
}
