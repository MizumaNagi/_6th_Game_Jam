using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFadein : MonoBehaviour
{
    [SerializeField] private FadeManager fade;
    // Start is called before the first frame update
    void Start()
    {
        fade.StartFadein();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
