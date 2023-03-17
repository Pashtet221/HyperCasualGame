using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance {get; private set;}

    [SerializeField] private string sceneName;
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

    public void SwitchScene()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.DOFade(1, 3)
            .OnComplete(() => { 
                SceneManager.LoadScene(sceneName);
             });
    }
}
