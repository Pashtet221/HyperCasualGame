using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLanguage : MonoBehaviour
{
    public static SelectLanguage instance {get; private set;}

    [HideInInspector] public int language;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        language = PlayerPrefs.GetInt("language", language);
    }

    public void RussuanLanguage()
    {
        language = 0;
        PlayerPrefs.SetInt("language", language);
    }

    public void EnglishLanguage()
    {
        language = 1;
        PlayerPrefs.SetInt("language", language);
    }
}
