using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraDoTween : MonoBehaviour
{
    public static CameraDoTween instance {get; private set;}

    public float duration;
    public Transform camera;
    public PathType pathType;
    public PathMode pathMode;

    public Vector3[] pathOpen;
    public Vector3[] pathClose;


    private void Awake()
    {
        instance = this;
    }

    public void OnShop()
    {
        camera.DOPath(pathOpen, duration, pathType, pathMode, 10, Color.red);
    }

    public void OffShop()
    {
        camera.DOPath(pathClose, duration, pathType, pathMode, 10, Color.red);
    }
}
