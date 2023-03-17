using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public static Hole instance {get; private set;}

    public ParticleSystem confetti;
    public GameObject glow;
    

    private void Start()
    {
        OffGlow();
    }

    private void Awake()
    {
        instance = this;
    }

    public void OnGlow()
    {
        glow.SetActive(true);
    }

    public void OffGlow()
    {
        glow.SetActive(false);
    }
}
