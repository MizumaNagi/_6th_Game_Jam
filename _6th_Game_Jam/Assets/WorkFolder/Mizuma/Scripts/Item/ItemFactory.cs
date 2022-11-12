using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : SingletonClass<ItemFactory>
{
    [SerializeField] private GameObject[] enemyPrefabArr;
    [SerializeField] private GameObject[] allyPrefabArr;
    [SerializeField] private GameObject castlePrefab;
    [SerializeField] private GameObject healPrefab;

    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform enemyLargeParent;
    [SerializeField] private Transform healParent;

    private void Start()
    {
        ItemFactory.Instance.CreateItem(Item.ItemType.Heal, 1, new Vector3(0f, 0f, 56f));
        ItemFactory.Instance.CreateItem(Item.ItemType.Heal, 2, new Vector3(-1f, 0f, 62f));
        ItemFactory.Instance.CreateItem(Item.ItemType.Heal, 3, new Vector3(1f, 0f, 68f));

        ItemFactory.Instance.CreateItem(Item.ItemType.Enemy, 1, new Vector3(0f, 0f, 84f));
        ItemFactory.Instance.CreateItem(Item.ItemType.Enemy, 2, new Vector3(-1f, 0f, 88f));
        ItemFactory.Instance.CreateItem(Item.ItemType.Enemy, 2, new Vector3(1f, 0f, 88f));
        ItemFactory.Instance.CreateItem(Item.ItemType.Enemy, 9999, new Vector3(0f, 0f, 100f));

        ItemFactory.Instance.CreateItem(Item.ItemType.Heal, 2, new Vector3(0f, 0f, 120f));
        ItemFactory.Instance.CreateItem(Item.ItemType.Heal, 1, new Vector3(-1f, 0f, 126f));
        ItemFactory.Instance.CreateItem(Item.ItemType.Heal, 4, new Vector3(1f, 0f, 126f));
        ItemFactory.Instance.CreateItem(Item.ItemType.Heal, 2, new Vector3(0f, 0f, 132f));
    }

    public GameObject CreateItem(Item.ItemType type, int effectPower, Vector3 pos, Vector3? rot = null)
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
            newItem.GetComponent<AnimationController>().OnItem();
        }
        else if(type == Item.ItemType.Enemy_Large)
        {
            newItem = Instantiate(castlePrefab, pos, Quaternion.Euler(targetRot)).transform;
            for(int i = 1; i < 6; i++)
            {
                int rnd = Random.Range(0, enemyPrefabArr.Length);
                Transform newEnemy = Instantiate(enemyPrefabArr[rnd], newItem.GetChild(i).position, newItem.GetChild(i).rotation).transform;
                newEnemy.SetParent(newItem);
                Item item = newEnemy.GetComponent<Item>();
                item.powerText.enabled = false;
                item.enabled = false;
                newEnemy.GetComponent<BoxCollider>().enabled = false;
                newEnemy.GetComponent<MoveMode>().enabled = false;
                Destroy(newEnemy.GetComponent<Rigidbody>());
                newEnemy.GetComponent<AnimationController>().OnItem();
            }
            itemCompo = newItem.GetComponent<Item>();
            newItem.transform.SetParent(enemyLargeParent);
        }
        else if(type == Item.ItemType.Heal)
        {
            newItem = Instantiate(healPrefab, pos, Quaternion.Euler(targetRot)).transform;
            newItem.SetParent(healParent);
            
            itemCompo = newItem.GetComponent<Item>();

            for(int i = 0; i < effectPower; i++)
            {
                int rnd = Random.Range(0, allyPrefabArr.Length);
                Transform child = Instantiate(allyPrefabArr[rnd], pos, Quaternion.Euler(targetRot)).transform;
                child.SetParent(newItem);
                child.GetComponent<AnimationController>().OnItem();
            }

            /*
            int rnd = Random.Range(0, allyPrefabArr.Length);
            newItem = Instantiate(allyPrefabArr[rnd], pos, Quaternion.Euler(targetRot)).transform;
            newItem.SetParent(healParent);

            BoxCollider collider = newItem.gameObject.AddComponent<BoxCollider>();
            collider.center = new Vector3(0.5f, 7.5f, 0f);
            collider.size = new Vector3(10f, 15f, 5f);
            collider.isTrigger = true;

            itemCompo = newItem.gameObject.AddComponent<Item>();
            itemCompo.type = Item.ItemType.Heal;
            */
        }
        
        itemCompo.Init(effectPower);

        return newItem.gameObject;
    }
}
