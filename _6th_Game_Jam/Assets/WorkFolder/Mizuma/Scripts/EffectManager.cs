using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonClass<EffectManager>
{
    [SerializeField] private GameObject[] effectPrefabs;

    private Transform effectsParent;

    private void Start()
    {
        effectsParent = new GameObject("EffectParent").transform;
    }

    public void PlayEffect(EffectType type, Vector3 createPos, Transform parent = null)
    {
        Transform newEffect = Instantiate(effectPrefabs[(int)type], createPos, Quaternion.identity).transform;
        if (parent == null) newEffect.SetParent(effectsParent);
        else newEffect.SetParent(parent);
    }

    public enum EffectType
    {
        Run_Smoke,
        Take_Damage,
        On_Damage,
        Death_Enemy,
        Collect_Item
    }
}
