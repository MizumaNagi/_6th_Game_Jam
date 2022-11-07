using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonClass<PlayerManager>
{
    [SerializeField] private ChildFactory childFactory;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private CameraController camCon;

    public ChildFactory ChildFactory => childFactory;
    public PlayerData PlayerData => playerData;

    private void Start()
    {
        camCon.ManagedStart();
        playerMove.ManagedStart();
        childFactory.BirthChild(1);
    }

    private void FixedUpdate()
    {
        playerMove.ManagedFixedUpdate();
    }

    public void BirthChild(int num)
    {
        SoundManager.Instance.PlaySE(SEName.Collect_Item);
        EffectManager.Instance.PlayEffect(EffectManager.EffectType.Collect_Item, new Vector3(transform.position.x, 1.4f, transform.position.z));

        playerData.HealHp(num);
        childFactory.BirthChild(num);
        camCon.UpdateCameraView(playerData.PlayerHp);
    }

    public void BirthChild(GameObject[] catchItems)
    {
        SoundManager.Instance.PlaySE(SEName.Collect_Item);
        EffectManager.Instance.PlayEffect(EffectManager.EffectType.Collect_Item, new Vector3(transform.position.x, 1.4f, transform.position.z));

        playerData.HealHp(catchItems.Length);
        childFactory.RegisterChild(catchItems);
        camCon.UpdateCameraView(playerData.PlayerHp);
    }

    public void KillChild(int num)
    {
        SoundManager.Instance.PlaySE(SEName.On_Damage);
        EffectManager.Instance.PlayEffect(EffectManager.EffectType.On_Damage, new Vector3(transform.position.x, 1.4f, transform.position.z));

        bool isDeath = playerData.TakeDamage(num);

        int afterNum = childFactory.GetCanKillChildCnt(num);
        if (isDeath == true)
        {
            Debug.Log("<color=red>Game Over !</color>");
            GameManager.Instance.GameEnd();
        }

        childFactory.KillChild(afterNum);
        camCon.UpdateCameraView(playerData.PlayerHp);
    }

    public void StartMove()
    {
        playerMove.isStop = false;
    }

    public void StartStop()
    {
        playerMove.isStop = true;
    }

    public void GameEnd()
    {
        StartCoroutine(playerMove.GameEnd());
    }
}
