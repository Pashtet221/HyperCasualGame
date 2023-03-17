using UnityEngine;
using UnityEngine.Events;
using VavilichevGD.Utils.Timing.Example;

public class UIZoomImage : MonoBehaviour
{
    private Vector3 initialScale;

    [SerializeField]
    private float zoomSpeed = 0.1f;
    [SerializeField]
    private float maxZoom = 10f;

    private AnswerScript answerScript;


    private void Awake()
    {
        answerScript = FindObjectOfType<AnswerScript>();

        initialScale = transform.localScale;
    }

    public void OnScroll()
    {
        var delta = Vector3.one * zoomSpeed;
        var desiredScale = transform.localScale -= delta;

        desiredScale = ClampDesiredScale(desiredScale);

        transform.localScale = desiredScale;

        WidgetTimerExample.ResetScrollEvent.AddListener(ResetScroll);
    }

    public void ResetScroll()
    {
        transform.localScale = new Vector3(10f, 10f, 10f);
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(initialScale / maxZoom, desiredScale);
        desiredScale = Vector3.Min(initialScale, desiredScale);
        return desiredScale;
    }
}