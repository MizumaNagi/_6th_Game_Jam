using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera myCamera;
    private float initFOV;
    private Vector3 initPos;
    private Vector2 LeaveVecEachCharacter { get { return new Vector2(-0.45f, 0.25f); } }

    public void ManagedStart()
    {
        myCamera = GetComponent<Camera>();
        initFOV = myCamera.fieldOfView;
        initPos = transform.localPosition;
    }
    public void UpdateCameraView(int characterLength, float addSizeEachCharacter)
    {
        myCamera.fieldOfView = initFOV + characterLength * addSizeEachCharacter;
    }

    public void UpdateCameraView(int characterLength)
    {
        Vector2 totalMoveVec = characterLength * LeaveVecEachCharacter;
        transform.localPosition = initPos + new Vector3(0, totalMoveVec.y, totalMoveVec.x);

        float rotX = 35f + characterLength * 0.2f;
        rotX = Mathf.Min(rotX, 45f);
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);
    }
}
