using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using VavilichevGD.Utils.Timing.Example;

public class AnswerScript : MonoBehaviour
{
    public static AnswerScript instance {get; private set;}
    
    [HideInInspector]
    public bool isCorrect = false;
    
    [Header("Colors")]
    [SerializeField] public Color startColor, chooseAnswerColor, redColor, greenColor;
    
    private float timeToNextQuestion;

    [SerializeField] private Button nextQuestionButton;
    
    [Header("Holes")]
    [SerializeField] public Hole hole;

    [HideInInspector]
    public Renderer holeRenderer;

    private AudioManager audio;

    public static UnityEvent ChooseAnswerEvent = new UnityEvent();

    private Enemy enemy;



    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        nextQuestionButton.onClick.AddListener(HoleStartColor);
    }

    private void OnDisable()
    {
        nextQuestionButton.onClick.RemoveListener(HoleStartColor);
    }

    private void Start()
    {
        timeToNextQuestion = WidgetTimerExample.instance._remainingSeconds;
        ChangeImageColor(startColor);
        holeRenderer = hole.GetComponent<Renderer>();
        audio = FindObjectOfType<AudioManager>();
        enemy = FindObjectOfType<Enemy>();
    }
    

    public void Answer()
    {
        ChooseAnswerEvent?.Invoke();

        if(isCorrect)
        {
            StartCoroutine(CorrectAnswer());
        }
        else
        {
            StartCoroutine(WrongAnswer());
        }
        StartCoroutine(NoSelectAnswer());

        Bar.instance.EmptyBar(timeToNextQuestion);
    }


    private IEnumerator CorrectAnswer()
    {
        hole.OnGlow();
        ChangeImageColor(chooseAnswerColor);
        yield return new WaitForSeconds(timeToNextQuestion);
        audio.Play("Victory");
        MyPlayer.instance.Dance(true);
        //Enemy.instance.Dance();
       // enemy.OnAnimationActivate += AiDance;
        hole.OffGlow();
        HoleGreenColor();
        ChangeImageColor(greenColor);
        QuizManager.instance.Correct();
    }

    private IEnumerator WrongAnswer()
    {
        hole.OnGlow();
        ChangeImageColor(chooseAnswerColor);
        yield return new WaitForSeconds(timeToNextQuestion);
        audio.Play("Looser");
        hole.OffGlow();
        HoleRedColor();
        ChangeImageColor(redColor);
        QuizManager.instance.Wrong();
    }


    private IEnumerator NoSelectAnswer()
    {
        ChangeImageColor(chooseAnswerColor);
        yield return new WaitForSeconds(timeToNextQuestion);
        QuizManager.instance.NoSelectAnswers();
    }

    private IEnumerator OpenGateway()
    {
        yield return new WaitForSeconds(1.5f);
        DisableHole();
        MyPlayer.instance.DisablePlayerPhysics();
        Enemy.instance.DisableAIPhysics();
    }

    private void ChangeImageColor(Color color)
    {
        GetComponent<Image>().color = color;
    }

    private void ChangeHoleColor(Color color)
    {
        holeRenderer.material.color = color;
    }

    public void HoleStartColor()
    {
        ChangeHoleColor(chooseAnswerColor);
        MyPlayer.instance.Dance(false);
        
        Bar.instance.FillBar(0,1);
    }

    public void HoleGreenColor()
    {
       ChangeHoleColor(greenColor);
       hole.confetti.Play();
       Invoke("Rotate", 1.5f);
    }

    private void Rotate()
    {
        MyPlayer.instance.RotatePlayer();
    }

    public void HoleRedColor()
    {
        ChangeHoleColor(redColor);
        StartCoroutine(OpenGateway());
    }


    // private void EnableHole()
    // {
    //     hole.SetActive(true);
    // }

    private void DisableHole()
    {
        hole.gameObject.SetActive(false);
    }

    public void NoSelectQuestion()
    {
        StartCoroutine(NoSelectOpenGateway());
    }

    private IEnumerator NoSelectOpenGateway()
    {
        yield return new WaitForSeconds(1.5f);
        DisableHole();
    }

    // private void AiDance()
    // {
    //     enemy.Dance(true);
    // }
}
