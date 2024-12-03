using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	[SerializeField] CanvasGroup m_canvasGroup;

	Canvas canvas;	
	RectTransform rectTransform;
	Image image;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		m_canvasGroup = GetComponent<CanvasGroup>();
		image = GetComponent<Image>();
		canvas = FindObjectOfType<Canvas>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		image.color = new Color(225, 225, 225, 170);
		m_canvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		m_canvasGroup.blocksRaycasts = true;
		image.color = new Color(225, 225, 225, 225);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//Debug.Log("OnPointerDown");
	}
}
