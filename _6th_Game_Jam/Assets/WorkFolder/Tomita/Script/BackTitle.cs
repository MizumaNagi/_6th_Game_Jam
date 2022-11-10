using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackTitle : MonoBehaviour
{
    [SerializeField] private FadeManager fade;
    private IEnumerator Scenefade()
    {
        fade.SceneMove();
        yield return new WaitWhile(() => fade.Fadeout == true);
        SceneManager.LoadScene("TitleScene");
    }
    // Start is called before the first frame update
    public void ButtonClick()
    {
        StartCoroutine(Scenefade());
    }
    // Start is called before the first frame update
    
}
