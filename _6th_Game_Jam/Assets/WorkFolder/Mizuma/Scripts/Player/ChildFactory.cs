using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFactory : MonoBehaviour
{
    [SerializeField] private Transform childParent;
    [SerializeField] private GameObject childPrefab;
    [SerializeField] private GameObject[] allyPrefabArr;

    public void BirthChild(int num)
    {
        for(int i = 0; i < num; i++)
        {
            int rnd = Random.Range(0, allyPrefabArr.Length);

            Transform newChild = Instantiate(allyPrefabArr[rnd]).transform;
            newChild.SetParent(childParent);
            newChild.position = new Vector3(100000f, newChild.position.y, 0f);
        }
    }

    public void KillChild(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Destroy(childParent.GetChild(i).gameObject);
        }
    }

    public int GetCanKillChildCnt(int num)
    {
        if (childParent.childCount < num) return childParent.childCount;
        else return num;
    }
}
