using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject hintBoard;
    [SerializeField] private MeshRenderer boardRender;
    [SerializeField] private Animator boardAnim;
    [SerializeField] private Material[] hintMatArr;
    [SerializeField] private Material escapeHintMat;

    [SerializeField] private FadeManager fadeManager;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private MapController mapCon;

    private readonly Vector3 distanceToPlayer = new Vector3(1.67999995f, 1.722f, 5.82000017f);
    private const string aniLeaveName = "triLeave";
    private const string aniGroundName = "triGround";

    public bool isOnceWarp = false;
    public int currentHintIndex = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isOnceWarp == false)
            {
                StartCoroutine(fadeManager.PureFadeInOut(() =>
                {
                    playerMove.SkipTutorial();
                    mapCon.SkipTutorial();
                }));
                isOnceWarp = true;
            }
        }

        //hintBoard.transform.position = playerMove.transform.position + distanceToPlayer;
    }

    public void NextEnableHint()
    {
        Debug.Log("Next");
        boardAnim.SetTrigger(aniGroundName);
        boardRender.material = hintMatArr[currentHintIndex];

    }

    public void EndCurrentHint()
    {
        Debug.Log("End");
        boardAnim.SetTrigger(aniLeaveName);
        currentHintIndex++;
        if (currentHintIndex == hintMatArr.Length) EndTutorial();
    }

    private void EndTutorial()
    {
        StartCoroutine(DelayDisable());
        playerMove.EndTutorial();
    }

    private IEnumerator DelayDisable()
    {
        yield return new WaitForSeconds(3f);
        playerMove.enabled = false;
        this.gameObject.SetActive(false);
        hintBoard.SetActive(false);
    }
}
