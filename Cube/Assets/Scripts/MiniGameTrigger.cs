using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
	[SerializeField] GameObject prefab;

	GameObject miniGamePopup;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && miniGamePopup == null)
		{
			var canvas = FindObjectOfType<Canvas>();
			miniGamePopup = Instantiate(prefab, canvas.transform);
		}
	}
}
