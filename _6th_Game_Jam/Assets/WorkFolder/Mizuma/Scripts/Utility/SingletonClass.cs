using System;
using UnityEngine;

public abstract class SingletonClass<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected bool canLiveSceneOver = false;
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError(typeof(T) + "ÇÕÇ†ÇËÇ‹ÇπÇÒÅB");
                return null;
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        if (instance == null) instance = this as T;
        else Destroy(this);

        if (canLiveSceneOver == true) DontDestroyOnLoad(this.gameObject);
    }

}