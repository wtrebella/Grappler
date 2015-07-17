using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	[SerializeField] private Anchorable anchorablePrefab;
	[SerializeField] private float distanceBetweenAnchorablesX = 5;
	[SerializeField] private float minY = 16;
	[SerializeField] private float maxY = 83;

	private List<Anchorable> anchorables;

	private void Awake() {
		anchorables = new List<Anchorable>();

		for (int i = 0; i < 1000; i++) AddAnchorable();
	}

	private void AddAnchorable() {
		Anchorable anchorable = Instantiate(anchorablePrefab) as Anchorable;
		anchorable.transform.parent = transform;
		int anchorableCount = anchorables.Count;
		float rangeY = maxY - minY;
		Vector3 position = Vector3.zero;
		position.x = distanceBetweenAnchorablesX * anchorableCount;
		position.y = Mathf.PerlinNoise((float)anchorableCount / 20 + 0.01f, 0) * rangeY + minY;
		anchorable.transform.position = position;
		anchorables.Add(anchorable);

	}
}
