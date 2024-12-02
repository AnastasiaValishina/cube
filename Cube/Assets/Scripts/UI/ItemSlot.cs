using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag != null)
		{
			var targetPosition = GetComponent<RectTransform>().anchoredPosition;
			eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = targetPosition;
		}
	}
}
