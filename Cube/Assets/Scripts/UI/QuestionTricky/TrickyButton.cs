using UnityEngine;
using UnityEngine.UI;

public class TrickyButton : MonoBehaviour
{
	[SerializeField] Image paw;

	Button _button;
	TrickyQuestion _trickyQuestion;

	private void Awake()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(OnButtonClick);
		paw.gameObject.SetActive(false);	
	}

	private void Start()
	{
		_trickyQuestion = FindObjectOfType<TrickyQuestion>();
	}

	private void OnButtonClick()
	{
		_trickyQuestion.AnswerSelected(this);
	}

	public void ShowPaw()
	{
		paw.gameObject.SetActive(true);
	}
}
