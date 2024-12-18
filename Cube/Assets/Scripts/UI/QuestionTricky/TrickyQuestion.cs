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
	bool _gameOver = false;

    public bool GameOver { get => _gameOver; private set => _gameOver = value; }

    private void Awake()
	{
		_closeBtn.onClick.AddListener(Close);
		_winText.gameObject.SetActive(false);
		_closeBtn.gameObject.SetActive(false);
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

	public void TriggerPawIfCorrect(TrickyButton button)
	{
		//if (GameOver) return;

		if (button == _correctButton)
		{
			if (!_pawSpawned)
			{
				_wrongButtons[0].ShowPaw();
				_pawSpawned = true;
			}
		}
	}

	public void SendAnswer(TrickyButton button)
	{
		if (GameOver) return;

		if (button == _correctButton)
		{
			Win();
		}
		else 
		{
			Lose();
		}
		GameOver = true;
	}

	private void Lose()
	{
		_winText.gameObject.SetActive(true);
		_winText.text = "You lost!";
		_closeBtn.gameObject.SetActive(true);
	}

    public void CheckAnswer(TrickyButton button)
    {
        if (GameOver) return;

        if (button != _correctButton)
        {
			button.ShowLose();
            Lose();
			GameOver = true;
        }
    }
}
