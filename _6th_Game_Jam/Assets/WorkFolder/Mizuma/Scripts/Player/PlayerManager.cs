using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonClass<PlayerManager>
{
    [SerializeField] private ChildFactory childFactory;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private CameraController camCon;

    public ChildFactory ChildFactory => childFactory;
    public PlayerData PlayerData => playerData;

    private void Start()
    {
        camCon.ManagedStart();
        BirthChild(10);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) BirthChild(10);
    }

    public void BirthChild(int num)
    {
        playerData.playerLength += num;
        childFactory.BirthChild(num);
        camCon.UpdateCameraView(playerData.playerLength);
    }

    public void KillChild(int num)
    {
        playerData.playerLength -= num;
        childFactory.KillChild(num);
    }
}
