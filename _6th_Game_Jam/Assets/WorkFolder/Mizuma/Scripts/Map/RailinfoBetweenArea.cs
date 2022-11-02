using UnityEngine;

/// <summary>
/// �u���b�N��̃}�b�v�Ő������U��������ׂɁA�K�v�Ȉʒu���W�̏W�܂�
/// </summary>
public class RailinfoBetweenArea : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 startHandlePos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private Vector3 endHandlePos;

    public void SetProperty(Vector3 startPos, Vector3 startHandlePos, Vector3 endPos, Vector3 endHandlePos)
    {
        this.startPos = startPos;
        this.startHandlePos = startHandlePos;
        this.endPos = endPos;
        this.endHandlePos = endHandlePos;
    }

    public Vector3[] GetProperty()
    {
        Vector3[] result = new Vector3[4];
        result[0] = startPos;
        result[1] = startHandlePos;
        result[3] = endPos;
        result[2] = endHandlePos;
        return result;
    }
}
