using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadealpha;
    private float alpha;
    private bool fadeOut;
    private bool fadein;
    public bool isStartFadeIn;

    public bool Fadeout
    {
        get
        {
            return fadeOut;
        }
    }
    public bool Fadein
    {
        get
        {
            return fadein;
        }
    }

    public GameObject panelFade;
    public string nextScene;

    private void Start()
    {
        if(isStartFadeIn == true)
        {
            StartFadein();
        }
    }

    void Update()
    {
        if (fadein == true)
        {
            FadeIn();
        }
        if (fadeOut == true)
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        alpha -= Time.deltaTime / 2;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if (alpha <= 0)
        {
            fadein = false;
            panelFade.SetActive(false);
        }
    }

    void FadeOut()
    {
        alpha += Time.deltaTime / 2;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if (alpha >= 1)
        {
            StartFadein();
            SceneManager.LoadScene(nextScene);
        }
    }

    public void SceneMove(bool b)
    {
        if (b) nextScene = "Main";
        else nextScene = "Title";

        fadein = false;
        fadeOut = true;
        panelFade.SetActive(true);
        alpha = 0;
    }

    public void StartFadein()
    {
        fadein = true;
        fadeOut = false;
        panelFade.SetActive(true);
        alpha = 1;
    }
}
