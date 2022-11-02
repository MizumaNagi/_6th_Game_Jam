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
                Debug.LogError(typeof(T) + "�͂���܂���B");
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
                " �͊��ɑ���GameObject�ɃA�^�b�`����Ă��邽�߁A�R���|�[�l���g��j�����܂���." +
                " �A�^�b�`����Ă���GameObject�� " + Instance.gameObject.name + " �ł�.");
            return;
        }

        if (canLiveSceneOver == true) DontDestroyOnLoad(this.gameObject);
    }

}