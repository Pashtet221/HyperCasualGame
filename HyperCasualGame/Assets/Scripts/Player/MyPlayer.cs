using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public class MyPlayer : MonoBehaviour
{
    public static MyPlayer instance { get; private set; }
    
    [SerializeField] private Transform startPos;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Transform[] points;
    private Dictionary<Button, Transform> buttonToPointMap;

    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody rb;

    [HideInInspector]public float speed;

    public event Action<Transform> OnMoveTo;



    private void Awake()
    {
       instance = this;

       agent = GetComponent<NavMeshAgent>();
       animator = GetComponent<Animator>();
       rb = GetComponent<Rigidbody>();

       // Populate the buttonToPointMap dictionary
    buttonToPointMap = new Dictionary<Button, Transform>();
    for (int i = 0; i < buttons.Length; i++)
    {
        buttonToPointMap[buttons[i]] = points[i];
        buttons[i].onClick.AddListener(() => OnMoveTo?.Invoke(points[i]));
    }
    }

    private void Start()
    {
        transform.position = startPos.position;
    }


    public void Dance(bool isDance)
    {
        animator.SetBool("Dancing", isDance);
    }


    public void MoveTo(Button button)
    {
       agent.SetDestination(buttonToPointMap[button].position);
    }



    public void DisablePlayerPhysics()
    {
        agent.enabled = false;
        rb.isKinematic = false;
    }

    public void RotatePlayer()
    {
        transform.DORotate(new Vector3(0,180,0), 0.5f, RotateMode.Fast);
    }

    private void OnDisableButtons()
    {
       for (int i = 0; i < buttons.Length; i++)
       {
           buttons[i].interactable = false;
       }
    }

    public void RespawnPlayer()
    {
        transform.position = startPos.transform.position;
        transform.rotation = Quaternion.identity;
        agent.SetDestination(startPos.position);
    }
}
