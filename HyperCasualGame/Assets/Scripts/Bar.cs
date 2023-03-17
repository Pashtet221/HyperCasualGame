using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Bar : MonoBehaviour
{
    public static Bar instance {get; private set;}

    public Image bar;
    private float time;
    private float startTimeScale;

    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       FillBar(0, 1);
    }

    public void EmptyBar(float t)
    {
        time = t;
        bar.DOFillAmount(0, t);
    }
    
    public void FillBar(float t, float _timeScale)
    {
        time = t;
        startTimeScale = _timeScale;
        bar.DOFillAmount(_timeScale, t);
    }
}
