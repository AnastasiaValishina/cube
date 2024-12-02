using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
	[SerializeField] GameObject prefab;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			var canvas = FindObjectOfType<Canvas>();	
			Instantiate(prefab, canvas.transform);
		}
	}
}
