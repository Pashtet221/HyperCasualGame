using UnityEngine;
using TMPro;

public class LanguageText : MonoBehaviour
{
    public int language;
    public string[] text;
    private TMP_Text textLine;

    private void Awake()
    {
        textLine = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        language = PlayerPrefs.GetInt("language", language);
        textLine.text = "" + text[language];
    }
}
