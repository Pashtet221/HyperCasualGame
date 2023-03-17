using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance {get; private set;}

    private int currentLevelIndex = 3;
    [SerializeField] private Image fadeScreen;

    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.DOFade(0, 3).OnComplete(() => { fadeScreen.gameObject.SetActive(false); });
    }

    private void Update()
    {
        currentLevelIndex = 3;

        if(PlayerPrefs.HasKey("levelIndex"))
        {
            currentLevelIndex = PlayerPrefs.GetInt("levelIndex");
        }
    }



    public void StartLevel()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.DOFade(1, 3)
            .OnComplete(() => { 
                SceneManager.LoadScene(currentLevelIndex);
             });
    }

    public void LoadLevel()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.DOFade(1, 3)
            .OnComplete(() => { 
                SceneManager.LoadScene(currentLevelIndex);
             });
             currentLevelIndex += 1;
                PlayerPrefs.SetInt("levelIndex", currentLevelIndex);
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted! ...");
    }
}
