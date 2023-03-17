using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonScale : MonoBehaviour
{
    public static ButtonScale instance {get; private set;}

    private Vector3 _originalScale;
    private Vector3 _scaleTo;

    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.15f;
        transform.DOScale(transform.localScale, 1f).SetEase(Ease.InOutSine);
    }

    public void Scale()
    {
        transform.DOScale(_scaleTo, 1f).SetEase(Ease.InOutSine);
    }

    public void ReturnScale()
    {
        transform.localScale = new Vector3(1,1,1);
    }

}
