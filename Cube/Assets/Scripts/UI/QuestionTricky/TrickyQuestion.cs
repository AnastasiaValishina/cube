using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrickyQuestion : MonoBehaviour
{
	[SerializeField] TrickyButton _correctButton;
	[SerializeField] TrickyButton[] _wrongButtons;
	[SerializeField] TextMeshProUGUI _winText;
	[SerializeField] Button _closeBtn;

	bool _pawSpawned = false;

	private void Awake()
	{
		_closeBtn.onClick.AddListener(Close);
		_winText.gameObject.SetActive(false);
		_closeBtn.gameObject.SetActive(false);
	}

	public void Check()
	{
		//      foreach (var slot in _dropSlots)
		//      {
		//	if (!slot.IsCorrect()) return;
		//      }
		//Win();
	}

	void Win()
	{
		_winText.gameObject.SetActive(true);
		_closeBtn.gameObject.SetActive(true);
	}

	void Close()
	{
		Destroy(this.gameObject);
	}

	public void AnswerSelected(TrickyButton button)
	{
		if (button == _correctButton)
		{
			if (!_pawSpawned)
				_wrongButtons[0].ShowPaw();

			// fil animation
		}
	}
}
