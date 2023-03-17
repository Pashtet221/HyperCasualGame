using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class QuestionsAndAnswers 
{
    public string[] Question;
    public Answers[] answers;
    public int CorrectAnswer;
    public Sprite img;
}


[System.Serializable]
public class Answers
{
    public string[] languages;
}

