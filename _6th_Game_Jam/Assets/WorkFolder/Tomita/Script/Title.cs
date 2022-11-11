using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private FadeManager fade;

    private bool isOnce = false;

    private void Scenefade(bool b)
    {
        fade.SceneMove(b);
        isOnce = true;
        //yield return new WaitWhile(() => fade.Fadeout == true);
        //SceneManager.LoadScene("Main");
    }
    // Start is called before the first frame update
    public void ButtonClick(int i)
    {
        SoundManager.Instance.PlaySE(SEName.Push_Button);

        bool b = i != 0 ? true : false;
        if(isOnce == false) Scenefade(b);
    }
}
