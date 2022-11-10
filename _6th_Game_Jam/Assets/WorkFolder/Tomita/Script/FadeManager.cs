using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public GameObject Panalfade;
    Image fadealpha;
    private float alpha;
    private bool fadeout;
    private bool fadein;
    public bool Fadeout 
    {
        get 
        {
            return fadeout;
        }
    }
    public bool Fadein
    {
        get
        {
            return fadein;
        }
    }
    public int Main;
    // Start is called before the first frame update
    void Start()
    {
        fadealpha = Panalfade.GetComponent<Image>();
        alpha = fadealpha.color.a;
        fadein = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadein == true)
        {
            FadeIn();
        }
        if (fadeout == true)
        {
            FadeOut();
        }
    }
    void FadeIn()
    {
        alpha -= Time.deltaTime/3;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if (alpha <= 0)
        {
            fadein = false;
            Panalfade.SetActive(false);
        }
    }
    void FadeOut()
    {
        alpha += Time.deltaTime/3;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if (alpha >= 1)
        {
            fadeout = false;
            SceneManager.LoadScene("Fade" + Main);
        }
    }
        public void SceneMove()
        {
            fadeout = true;
            Panalfade.SetActive(true);
        alpha = 0;
        }
    public void StartFadein()
    {
        fadein = true;
        Panalfade.SetActive(true);
        alpha = 1;
    }
}
