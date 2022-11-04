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
    }

    private void Update()
    {
        playerMove.ManagedUpdate();
    }

    public void BirthChild(int num)
    {
        SoundManager.Instance.PlaySE(SEName.Collect_Item);
        EffectManager.Instance.PlayEffect(EffectManager.EffectType.Collect_Item, new Vector3(transform.position.x, 1.4f, transform.position.z));

        playerData.playerLength += num;
        childFactory.BirthChild(num);
        camCon.UpdateCameraView(playerData.playerLength);
    }

    public void KillChild(int num)
    {
        SoundManager.Instance.PlaySE(SEName.On_Damage);
        EffectManager.Instance.PlayEffect(EffectManager.EffectType.On_Damage, new Vector3(transform.position.x, 1.4f, transform.position.z));

        int afterNum = childFactory.GetCanKillChildCnt(num);
        if (num != afterNum)
        {
            Debug.Log("<color=red>Game Over !</color>");
            playerMove.isStop = true;
        }

        playerData.playerLength -= afterNum;
        childFactory.KillChild(afterNum);
        camCon.UpdateCameraView(playerData.playerLength);
    }
}
