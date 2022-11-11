using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackTitle : MonoBehaviour
{
    [SerializeField] private FadeManager fade;
    private IEnumerator Scenefade()
    {
        yield return new WaitWhile(() => fade.Fadeout == true);
        SceneManager.LoadScene("TitleScene");
    }
    // Start is called before the first frame update
    public void ButtonClick()
    {
        Destroy(SoundManager.Instance.gameObject);
        SceneManager.LoadScene("Title");
    }

    public void RetryClick()
    {
        SceneManager.LoadScene("Main");
        StartCoroutine(Scenefade());
    }
    // Start is called before the first frame update
    
}
