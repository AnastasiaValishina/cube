using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
	[SerializeField] Image targetImage;

	DragAndDropGame _dragAndDropGame;
	GameObject _setGameObject;

	public void Start()
	{
		_dragAndDropGame = FindObjectOfType<DragAndDropGame>();
	}
	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag != null)
		{
			var targetPosition = GetComponent<RectTransform>().anchoredPosition;
			eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = targetPosition;

			if (eventData.pointerDrag == targetImage.gameObject)
			{
				_setGameObject = eventData.pointerDrag;
			}
			_dragAndDropGame.Check();
		}
	}

	public bool IsCorrect()
	{
		if (_setGameObject == null) return false;
		if (_setGameObject == targetImage.gameObject)
		{
			return true;
		} 
		return false;
	}
}
