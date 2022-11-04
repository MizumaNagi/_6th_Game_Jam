using UnityEngine;

/// <summary>
/// ブロック状のマップで正しく誘導させる為に、必要な位置座標の集まり
/// </summary>
public class RailinfoBetweenArea : MonoBehaviour
{
    [SerializeField] private RailType railType;
    [SerializeField] private CurveType curveType;
    private Vector3 startPos;
    private Vector3 startHandlePos;
    private Vector3 endHandlePos;
    private Vector3 endPos;

    public void SetProperty()
    {
        Vector3 currentPos = transform.position;

        if(railType == RailType.Straight)
        {
            if (transform.rotation == Quaternion.Euler(Vector3.zero))
            {
                startPos = currentPos + new Vector3(0f, 0f, -5f);
                startHandlePos = currentPos + new Vector3(0f, 0f, -1.667f);
                endHandlePos = currentPos + new Vector3(0f, 0f, 1.667f);
                endPos = currentPos + new Vector3(0f, 0f, 5f);
            }
            else if (transform.rotation == Quaternion.Euler(new Vector3(0, 90, 0)))
            {
                startPos = currentPos + new Vector3(-5f, 0f, 0f);
                startHandlePos = currentPos + new Vector3(-1.667f, 0f, 0f);
                endHandlePos = currentPos + new Vector3(1.667f, 0f, 0f);
                endPos = currentPos + new Vector3(5f, 0f, 0f);
            }
            else if (transform.rotation == Quaternion.Euler(new Vector3(0, 180, 0)))
            {
                startPos = currentPos + new Vector3(0f, 0f, 5f);
                startHandlePos = currentPos + new Vector3(0f, 0f, 1.667f);
                endHandlePos = currentPos + new Vector3(0f, 0f, -1.667f);
                endPos = currentPos + new Vector3(0f, 0f, -5f);
            }
            else if (transform.rotation == Quaternion.Euler(new Vector3(0, 270, 0)))
            {
                startPos = currentPos + new Vector3(5f, 0f, 0f);
                startHandlePos = currentPos + new Vector3(1.667f, 0f, 0f);
                endHandlePos = currentPos + new Vector3(-1.667f, 0f, 0f);
                endPos = currentPos + new Vector3(-5f, 0f, 0f);
            }
            else Debug.LogError("Rotationは(0, 0/90/180/270, 0)にしてください。");
        }
        else
        {
            if (curveType == CurveType.Clock)
            {
                if (transform.rotation == Quaternion.Euler(Vector3.zero))
                {
                    startPos = currentPos + new Vector3(0f, 0f, 5f);
                    startHandlePos = currentPos + new Vector3(0f, 0f, 2.5f);
                    endHandlePos = currentPos + new Vector3(-2.5f, 0f, 0f);
                    endPos = currentPos + new Vector3(-5f, 0f, 0f);
                }
                else if (transform.rotation == Quaternion.Euler(new Vector3(0, 90, 0)))
                {
                    startPos = currentPos + new Vector3(5f, 0f, 0f);
                    startHandlePos = currentPos + new Vector3(2.5f, 0f, 0f);
                    endHandlePos = currentPos + new Vector3(0f, 0f, 2.5f);
                    endPos = currentPos + new Vector3(0f, 0f, 5f);
                }
                else if (transform.rotation == Quaternion.Euler(new Vector3(0, 180, 0)))
                {
                    startPos = currentPos + new Vector3(0f, 0f, -5f);
                    startHandlePos = currentPos + new Vector3(0f, 0f, -2.5f);
                    endHandlePos = currentPos + new Vector3(2.5f, 0f, 0f);
                    endPos = currentPos + new Vector3(5f, 0f, 0f);
                }
                else if (transform.rotation == Quaternion.Euler(new Vector3(0, 270, 0)))
                {
                    startPos = currentPos + new Vector3(-5f, 0f, 0f);
                    startHandlePos = currentPos + new Vector3(-2.5f, 0f, 0f);
                    endHandlePos = currentPos + new Vector3(0f, 0f, -2.5f);
                    endPos = currentPos + new Vector3(0f, 0f, -5f);
                }
                else Debug.LogError("Rotationは(0, 0/90/180/270, 0)にしてください。");
            }
            else if(curveType == CurveType.UntiClock)
            {
                if (transform.rotation == Quaternion.Euler(Vector3.zero))
                {
                    startPos = currentPos + new Vector3(-5f, 0f, 0f);
                    startHandlePos = currentPos + new Vector3(-2.5f, 0f, 0f);
                    endHandlePos = currentPos + new Vector3(0f, 0f, 2.5f);
                    endPos = currentPos + new Vector3(0f, 0f, 5f);
                }
                else if (transform.rotation == Quaternion.Euler(new Vector3(0, 90, 0)))
                {
                    startPos = currentPos + new Vector3(0f, 0f, 5f);
                    startHandlePos = currentPos + new Vector3(0f, 0f, 2.5f);
                    endHandlePos = currentPos + new Vector3(2.5f, 0f, 0f);
                    endPos = currentPos + new Vector3(5f, 0f, 0f);
                }
                else if (transform.rotation == Quaternion.Euler(new Vector3(0, 180, 0)))
                {
                    startPos = currentPos + new Vector3(5f, 0f, 0f);
                    startHandlePos = currentPos + new Vector3(2.5f, 0f, 0f);
                    endHandlePos = currentPos + new Vector3(0f, 0f, -2.5f);
                    endPos = currentPos + new Vector3(0f, 0f, -5f);
                }
                else if (transform.rotation == Quaternion.Euler(new Vector3(0, 270, 0)))
                {
                    startPos = currentPos + new Vector3(0f, 0f, -5f);
                    startHandlePos = currentPos + new Vector3(0f, 0f, -2.5f);
                    endHandlePos = currentPos + new Vector3(-2.5f, 0f, 0f);
                    endPos = currentPos + new Vector3(-5f, 0f, 0f);
                }
                else Debug.LogError("Rotationは(0, 0/90/180/270, 0)にしてください。");
            }
        }
    }

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

    public enum RailType
    {
        Straight,
        Curve
    }

    public enum CurveType
    {
        Clock,
        UntiClock,
        None
    }
}
