using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFactory : MonoBehaviour
{
    [SerializeField] private Transform childParent;
    [SerializeField] private GameObject childPrefab;
    [SerializeField] private GameObject[] allyPrefabArr;

    private const int LimitChildCnt = 100;

    public void BirthChild(int num)
    {
        for(int i = 0; i < num; i++)
        {
            int rnd = Random.Range(0, allyPrefabArr.Length);

            Transform newChild = Instantiate(allyPrefabArr[rnd]).transform;
            if (childParent.childCount <= 0) newChild.gameObject.GetComponent<AnimationController>().OnPlayer();

            newChild.SetParent(childParent);
            newChild.position = new Vector3(100000f, newChild.position.y, 0f);

        }
    }

    public void RegisterChild(GameObject[] addChilds)
    {
        for(int i = 0; i < addChilds.Length; i++)
        {
            Transform addChild = addChilds[i].transform;
            addChild.transform.SetParent(childParent);
            addChild.position = new Vector3(100000f, addChild.position.y, 0f);
            addChild.localScale *= 1f / 0.6f;
            addChild.rotation = Quaternion.identity;
            addChild.GetComponent<AnimationController>().OnPlayer();
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

    public int GetCanNotBirthChildCnt(int num) // num: 4
    {
        int remSpace = LimitChildCnt - childParent.childCount; // rem: 2
        if (num > remSpace) return num - remSpace;
        else return 0;
    }
}
