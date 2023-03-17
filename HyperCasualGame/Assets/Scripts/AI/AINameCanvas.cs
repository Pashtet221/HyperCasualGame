using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINameCanvas : MonoBehaviour
{
    [SerializeField] private Transform ai;

    private void Update()
    {
        transform.position = ai.position;
    }
}
