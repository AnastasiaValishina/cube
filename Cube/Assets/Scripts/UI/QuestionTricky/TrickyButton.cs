using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TrickyButton : MonoBehaviour
{
	[SerializeField] Image _paw;
	[SerializeField] Image _fillImage;

	Button _button;
	TrickyQuestion _trickyQuestion;
	Vector2 _targetPawsition;
	Vector2 _startPawsition;
	bool _pawTriggered;

	private void Awake()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(OnButtonClick);
		_pawTriggered = false;
	}

	private void Start()
	{
		_trickyQuestion = FindObjectOfType<TrickyQuestion>();
		_targetPawsition = _paw.rectTransform.anchoredPosition;
		_startPawsition = new Vector2(Screen.width / 2, -Screen.height / 2);
		_paw.gameObject.SetActive(false);
		_fillImage.fillAmount = 0;
	}

	private void OnButtonClick()
	{
		if (!_pawTriggered)
		{
			_trickyQuestion.TriggerPaw(this);
			_pawTriggered = true;
		}

		float currentFill = _fillImage.fillAmount;
		_fillImage.fillAmount = Mathf.Clamp01(currentFill + 0.05f);

		if (Mathf.Approximately(currentFill, 1f))
		{
			OnFillComplete();
		}
	}

	public void ShowPaw()
	{
		_paw.gameObject.SetActive(true);

		//Sequence tappingAnim = DOTween.Sequence();

		_paw.rectTransform
			.DOAnchorPos(_targetPawsition, 0.5f)
			.From(_startPawsition)
			.OnComplete(() =>
			{
				_paw.rectTransform
					.DOSizeDelta(new Vector2(247.5f, 585f), 0.2f)
					.SetEase(Ease.InCubic)
					.SetLoops(-1, LoopType.Yoyo)
					.OnStepComplete(() =>
					{
						float currentFill = _fillImage.fillAmount;
						_fillImage.fillAmount = Mathf.Clamp01(currentFill + 0.05f);

						if (Mathf.Approximately(currentFill, 1f))
						{
							OnFillComplete();
						}
					});
			});
	}

	private void OnFillComplete()
	{
		DOTween.Kill(_paw.rectTransform);
		_trickyQuestion.SendAnswer(this);
	}
}
