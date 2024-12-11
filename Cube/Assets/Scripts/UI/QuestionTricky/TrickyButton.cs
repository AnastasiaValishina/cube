using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrickyButton : MonoBehaviour
{
	[SerializeField] Image paw;
	[SerializeField] bool isCorrect;

	Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();
		paw.gameObject.SetActive(false);	
	}

	private void StartPawClicking()
	{
		if (!isCorrect) return;

		paw.gameObject.SetActive(true);
	}
}
