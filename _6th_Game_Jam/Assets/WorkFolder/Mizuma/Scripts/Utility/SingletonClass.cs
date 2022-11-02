using System;
using UnityEngine;

public abstract class SingletonClass<T> : MonoBehaviour where T : MonoBehaviour
{
    protected bool canLiveSceneOver = false;
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError(typeof(T) + "はありません。");
                return null;
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            Debug.LogError(
                typeof(T) +
                " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
            return;
        }

        if (canLiveSceneOver == true) DontDestroyOnLoad(this.gameObject);
    }

}