using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace VavilichevGD.Utils.Timing.Example
{
	public class WidgetTimerExample : MonoBehaviour
	{
		public static WidgetTimerExample instance {get; private set;}
		[SerializeField] private TimerType _timerType;
		[SerializeField] public float _remainingSeconds;
		[SerializeField] private Button _buttonStart;
		[SerializeField] private Button _buttonPause;
		[SerializeField] private Button _buttonStop;
		[Space] [SerializeField] private Text _textType;
		[SerializeField] private Text _textValue;

		private Color _colorPaused = Color.yellow;
		private Color _colorUnpaused = Color.white;
		private SyncedTimer _timer;

		[SerializeField] private Image timerBar;

        [Header("Events")]
		public static UnityEvent ResetScrollEvent = new UnityEvent();





		 private Vector3 initialScale;

    [SerializeField]
    private float zoomSpeed = 0.1f;
    [SerializeField]
    private float maxZoom = 10f;



		private void Awake()
		{
			instance = this;

			initialScale = transform.localScale;
			timerBar = GetComponent<Image>();

			UpdatePauseButtonState();
			UpdateTimerTypeField();

			_textValue.text = $"Value: {_remainingSeconds.ToString()}";
		}
		

		private void OnEnable()
		{
			_buttonStart.onClick.AddListener(OnStartButtonClick);
			_buttonPause.onClick.AddListener(OnPauseButtonClick);
			_buttonStop.onClick.AddListener(OnStopButtonClick);

			AnswerScript.ChooseAnswerEvent.AddListener(OnStartButtonClick);
		}

		private void OnDisable()
		{
			_buttonStart.onClick.RemoveListener(OnStartButtonClick);
			_buttonPause.onClick.RemoveListener(OnPauseButtonClick);
			_buttonStop.onClick.RemoveListener(OnStopButtonClick);

			AnswerScript.ChooseAnswerEvent.RemoveListener(OnStartButtonClick);
		}

		private void SubscribeOnTimerEvents()
		{
			_timer.TimerValueChanged += TimerValueChanged;
			_timer.TimerFinished += TimerFinished;
		}

		private void UnsubscribeFromTimerEvents()
		{
			_timer.TimerValueChanged -= TimerValueChanged;
			_timer.TimerFinished -= TimerFinished;
		}

		private void UpdatePauseButtonState()
		{
			if (_timer == null)
			{
				_buttonPause.image.color = _colorUnpaused;
				return;
			}

			var color = _timer.isPaused ? _colorPaused : _colorUnpaused;
			_buttonPause.image.color = color;

			var text = _timer.isPaused ? "Unpause" : "Pause";
			var textField = _buttonPause.GetComponentInChildren<Text>();
			textField.text = text;
		}

		private void UpdateTimerTypeField()
		{
			_textType.text = $"Type: {_timerType.ToString()}";
		}

		private void OnStartButtonClick()
		{
			if (_timer == null)
			{
				_timer = new SyncedTimer(_timerType);
				SubscribeOnTimerEvents();
			}

			UpdateTimerTypeField();
			_timer.Start(_remainingSeconds);
			UpdatePauseButtonState();
		}

		private void OnPauseButtonClick()
		{
			if (_timer == null)
				return;

			if (_timer.isPaused)
				_timer.Unpause();
			else
				_timer.Pause();

			UpdatePauseButtonState();
		}

		private void OnStopButtonClick()
		{
			if (_timer == null)
				return;

			_timer.Stop();
			UpdatePauseButtonState();
		}

		private void TimerFinished()
		{
			ResetScrollEvent?.Invoke();
			_textValue.text = "Value: Finished (0)";
			StartCoroutine(WaitForNext());
		}

        IEnumerator WaitForNext()
        {
            yield return new WaitForSeconds(3f);
			
			if(QuizManager.instance.isCorrect)
			{
				Debug.Log("YYYYYYYAAAA!");
				NextQuestion();
			}
			else
			{
				TryAgain();
			}				
        }

		private void NextQuestion()
		{
			QuizManager.instance.EnableNextQuestionPanel();
			ResetScroll();
		}

		private void TryAgain()
		{
			QuizManager.instance.EnableTryAgainPanel();
		}


		private void TimerValueChanged(float remainingSeconds, TimeChangingSource timeChangingSource)
		{
			_textValue.text = $"Value: {remainingSeconds.ToString()}";
			timerBar.fillAmount = remainingSeconds;


			var delta = Vector3.one * (remainingSeconds / 10);
            var desiredScale = transform.localScale -= delta;

            desiredScale = ClampDesiredScale(desiredScale);

            transform.localScale = desiredScale;
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
}