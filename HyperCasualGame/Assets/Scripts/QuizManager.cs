using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using VavilichevGD.Utils.Timing.Example;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance {get; private set;}

    [Header("Вопросы")]
    [Space(10)]
    [SerializeField] private List<QuestionsAndAnswers> QnA;
    [SerializeField] private GameObject[] options;
    private int currentQuestion;

    [Header("Панели")]
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject GoPanel;
    [SerializeField] private GameObject nextQuestionPanel;
    [SerializeField] private GameObject tryAgainPanel;

    [Header("Кнопки")]
    [SerializeField] private Button nextLevelButton;

    [Header("Текстовые поля")]
    public Text QuestionTxt;
    public Text ScoreTxt;
    [SerializeField] private TMP_Text[] roundUIText;
    [SerializeField] private TMP_Text rewardUI;

    private int totalQuastions = 0;
    private int current = 1;
    private int score;

    [HideInInspector]
    public bool isCorrect = false;

    [SerializeField] private Image lvlImage;

    public static UnityEvent CorrectAnswerEvent = new UnityEvent();
    public static UnityEvent WrongAnswerEvent = new UnityEvent();
    public static UnityEvent NextLevelEvent = new UnityEvent();


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        totalQuastions = QnA.Count;
        GoPanel.SetActive(false);
        GanerateQuestion();
        UpdateRoundUIText();
    }

    private void OnEnable()
	{
		nextLevelButton.onClick.AddListener(NextLevel);
	}

    private void OnDisable()
	{
		nextLevelButton.onClick.RemoveListener(NextLevel);
	}



    public void UpdateRoundUIText()
	{
		for (int i = 0; i < roundUIText.Length; i++) {
			SetRoundText (roundUIText[i], ("Round " + current).ToString());
		}
        current += 1;
	}

    void SetRoundText (TMP_Text textMesh, string text)
	{
        textMesh.text = text;
	}

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void GameOver()
    {
        quizPanel.SetActive(false);
       // GoPanel.SetActive(true);
        LevelManager.instance.LoadLevel();
        ScoreTxt.text = score + "/" + totalQuastions;
    }


    public void Correct()
    {
        CorrectAnswerEvent?.Invoke();
        
        isCorrect = true;
        score += 1;
        QnA.RemoveAt(currentQuestion);

        var coinsAmount = 20;
        rewardUI.text = coinsAmount.ToString();
                
        GameDataManager.AddCoins(coinsAmount);
        GameSharedUI.Instance.UpdateCoinsUIText();
    }

    public void Wrong()
    {
        WrongAnswerEvent?.Invoke();

        isCorrect = false;
        
        QnA.RemoveAt(currentQuestion);
    }


    private void NextLevel()
    {
        NextLevelEvent?.Invoke();
        GanerateQuestion();
        DisableNextQuestionPanel();

        UpdateRoundUIText();
        MyPlayer.instance.RespawnPlayer();
    }

    public void EnableNextQuestionPanel()
    {
        nextQuestionPanel.SetActive(true);
    }

    public void DisableNextQuestionPanel()
    {
        nextQuestionPanel.SetActive(false);
    }


    public void EnableTryAgainPanel()
    {
        tryAgainPanel.SetActive(true);
    }

    public void DisableTryAgainPanel()
    {
        tryAgainPanel.SetActive(false);
    }



    private void SetAnswers()
    {
        for(int i = 0;i < options.Length;i++)
        {
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].answers[i].languages[SelectLanguage.instance.language].ToString();
            options[i].GetComponent<AnswerScript>().hole.gameObject.SetActive(true);
            options[i].GetComponent<ButtonScale>().ReturnScale();

            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
            
        }
    }

    public void NoSelectAnswers()
    {
        for(int i = 0;i < options.Length;i++)
        {
            if(options[i].GetComponent<AnswerScript>().isCorrect == true)
            {
                options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().greenColor;
                options[i].GetComponent<AnswerScript>().holeRenderer.material.color = AnswerScript.instance.greenColor;
                options[i].GetComponent<AnswerScript>().hole.gameObject.SetActive(true);
            }

            if(options[i].GetComponent<AnswerScript>().isCorrect == false)
            {
                options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().redColor;
                options[i].GetComponent<AnswerScript>().holeRenderer.material.color = AnswerScript.instance.redColor;
                options[i].GetComponent<AnswerScript>().NoSelectQuestion();
            }
        }
    }


    private void GanerateQuestion()
    {
        if(QnA.Count > 0)
        {
            currentQuestion = Random.Range(0,QnA.Count);
            lvlImage.sprite = QnA[currentQuestion].img;
            QuestionTxt.text = QnA[currentQuestion].Question[SelectLanguage.instance.language].ToString();
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of questions");
            GameOver();
        }
    }
  
}
