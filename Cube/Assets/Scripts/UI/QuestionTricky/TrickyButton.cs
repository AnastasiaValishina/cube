using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TrickyButton : MonoBehaviour
{
	[SerializeField] Image _paw;
	[SerializeField] Image _fillImage;
	[SerializeField] TapAnimator _tapAnimator;

	Button _button;
	TrickyQuestion _trickyQuestion;
	Vector2 _targetPawsition;
	Vector2 _startPawsition;
	bool _pawTriggered;
	int _taps = 0;


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
        if (_trickyQuestion.GameOver) return;
        if (!_pawTriggered)
		{
			_trickyQuestion.TriggerPawIfCorrect(this);
			_pawTriggered = true;
		}

        _tapAnimator.transform.position = Input.mousePosition;
        _tapAnimator.Tap();

        _trickyQuestion.CheckAnswer(this);
        ShowFill();
    }

    public void ShowPaw()
    {
        _paw.gameObject.SetActive(true);

        _paw.rectTransform
            .DOAnchorPos(_targetPawsition, 0.5f)
            .From(_startPawsition)
            .OnComplete(() => StartPawAnimation());
    }

    private void StartPawAnimation()
    {
        _paw.rectTransform
            .DOSizeDelta(new Vector2(247.5f, 585f), 0.3f)
            .SetEase(Ease.InCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .OnStepComplete(() => OnPawStepComplete());
    }

    private void OnPawStepComplete()
    {
        _taps++;
        if (_taps % 2 == 0) return;
        _tapAnimator.Tap();

        ShowFill();
    }

    private void ShowFill()
    {
        float currentFill = _fillImage.fillAmount;
        _fillImage.fillAmount = Mathf.Clamp01(currentFill + 0.05f);

        if (Mathf.Approximately(currentFill, 1f))
        {
            OnFillComplete();
        }
    }

    private void OnFillComplete()
	{
		DOTween.Kill(_paw.rectTransform);
		_trickyQuestion.SendAnswer(this);
	}

    public void ShowLose()
    {
        _fillImage.color = Color.red;
        _fillImage.fillAmount = 1;
    }
}
