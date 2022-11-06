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
        CreateItem(Item.ItemType.Heal, new Vector3(0, 0, 15), new Vector3(0f, 180f, 0f));
        CreateItem(Item.ItemType.Enemy, new Vector3(0, 0, 30), new Vector3(0f, 180f, 0f));
        CreateItem(Item.ItemType.Enemy_Large, new Vector3(0, 0, 45), new Vector3(0f, 180f, 0f));
    }

    public void CreateItem(Item.ItemType type, Vector3 pos, Vector3? rot = null)
    {
        Vector3 targetRot = rot ?? new Vector3(0f, 180f, 0f);

        if (type == Item.ItemType.Enemy)
        {
            int rnd = Random.Range(0, enemyPrefabArr.Length);
            Transform newEnemy = Instantiate(enemyPrefabArr[rnd], pos, Quaternion.Euler(targetRot)).transform;
            newEnemy.SetParent(enemyParent);
        }
        else if(type == Item.ItemType.Enemy_Large)
        {
            Transform newCastle = Instantiate(castlePrefab, pos, Quaternion.Euler(targetRot)).transform;
            for(int i = 1; i < 6; i++)
            {
                int rnd = Random.Range(0, enemyPrefabArr.Length);
                Transform newEnemy = Instantiate(enemyPrefabArr[rnd], newCastle.GetChild(i).position, newCastle.GetChild(i).rotation).transform;
                newEnemy.SetParent(newCastle);
                newEnemy.GetComponent<Item>().enabled = false;
                newEnemy.GetComponent<BoxCollider>().enabled = false;
            }
            newCastle.transform.SetParent(enemyLargeParent);
        }
        else if(type == Item.ItemType.Heal)
        {
            int rnd = Random.Range(0, allyPrefabArr.Length);
            GameObject newHeal = Instantiate(allyPrefabArr[rnd], pos, Quaternion.Euler(targetRot));
            newHeal.transform.SetParent(healParent);

            BoxCollider collider = newHeal.AddComponent<BoxCollider>();
            collider.center = new Vector3(0.5f, 7.5f, 0f);
            collider.size = new Vector3(10f, 15f, 5f);
            collider.isTrigger = true;

            newHeal.AddComponent<Item>().type = Item.ItemType.Heal;
        }
    }
}
