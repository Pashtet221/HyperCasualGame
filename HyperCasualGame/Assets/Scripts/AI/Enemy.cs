using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public static Enemy instance {get; private set;}

    [Header("MoveToPoint")]
    public Transform[] moveSpots;
    private int randomPoint;

    float randomX;
    float randomZ;

    Vector3 rand_point;

    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody rb;

    [Header("Sounds")]
    private AudioManager audio;

    [Header("Names")]
    [SerializeField] TMP_Text nameText;
    private readonly string[] names = {"Fred","George","Alena","Victoria","Pavel","228","123","@param","Max","Rafik"};

    public delegate void AnimationActivation();
    public event AnimationActivation OnAnimationActivate;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        audio = FindObjectOfType<AudioManager>();

        if(audio != null)
        {
            AppearanceSound();
        }

        if (nameText != null)
        {
            SetRandomName(nameText);
        }

        randomX = Random.Range (-1, 1f); 
        randomZ = Random.Range (-1, 1f);
        randomPoint = Random.Range (0, moveSpots.Length);
        
        var someRandomPoint = Random.insideUnitSphere + new Vector3(randomX, 0, randomZ);
        rand_point = moveSpots[randomPoint].position + someRandomPoint;

        StartCoroutine(Moveing());
    }

   IEnumerator Moveing()
   {
      yield return new WaitForSeconds(2f);
      agent.SetDestination(rand_point);
   }


    public void DisableAIPhysics()
    {
        agent.enabled = false;
        rb.isKinematic = false;
    }

    public void Dance(bool isDance)
    {
        animator.SetBool("isDancing", isDance);
        int randomValue = Mathf.RoundToInt(Random.Range(0f, 1f));
        animator.SetFloat("randDance", randomValue);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Destroy();
        }
        else if(collision.gameObject.tag == "Platform")
        {
            WaveSpawnner.DestroyGameObjectEvent.AddListener(Destroy);
        }
        else if(collision.gameObject.tag == "Hole" && QuizManager.instance.isCorrect)
        {
            Dance(true);
            Invoke("DisableAIPhysics", 2f);
        }
    }

    public void Destroy()
    {
        WaveSpawnner.DestroyGameObjectEvent.RemoveListener(Destroy);
        
        if(transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        GameSharedUI.Instance.UpdateAiAmoutUIText();
    }

    private void AppearanceSound()
    {
        audio.Play("EnemyAppearance");
    }

    private void SetRandomName(TMP_Text text)
    {
        int randomNameIndex = Random.Range(0, names.Length);
        string aiName = names[randomNameIndex];
        text.text = aiName;
    }
}
