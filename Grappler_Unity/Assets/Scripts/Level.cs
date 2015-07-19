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
		CreateAnchorables();
	}

	private void FixedUpdate() {
		CreateAnchorablesIfNeeded();
		RemoveOffscreenAnchorables();
	}

	private void CreateAnchorables() {
		while (true) {
			Vector3 position = GetNextAnchorablePosition();
			if (GameScreen.instance.GetIsOnscreenWithMarginX(position.x)) CreateAnchorable(position);
			else break;
		}
	}

	private static int anchorableNum = 0;
	private void CreateAnchorable(Vector3 position) {
		Anchorable anchorable = anchorablePrefab.Spawn();
		anchorable.transform.parent = transform;
		anchorable.transform.position = position;
		anchorables.Add(anchorable);
		anchorableNum++;
	}

	private Vector2 GetNextAnchorablePosition() {
		float rangeY = maxY - minY;
		float firstPerlinAmount = Mathf.PerlinNoise(0.01f, 0);
		float perlinAmount = Mathf.Abs(Mathf.PerlinNoise((float)anchorableNum / 5 + 0.01f, 0) - firstPerlinAmount);
		Vector2 position = Vector3.zero;
		position.x = distanceBetweenAnchorablesX * anchorableNum;
		position.y = perlinAmount * rangeY + minY;
		return position;
	}

	private void CreateAnchorablesIfNeeded() {	
		Vector2 nextAnchorablePosition = GetNextAnchorablePosition();
		if (GameScreen.instance.GetIsOnscreenWithMarginX(nextAnchorablePosition.x)) CreateAnchorable(nextAnchorablePosition);
	}

	private void RemoveOffscreenAnchorables() {
		float minX = GameScreen.instance.lowerLeftWithMargin.x;
		while (true) {
			Anchorable anchorable = anchorables[0];
			if (anchorable.transform.position.x >= minX) break;
			
			anchorables.RemoveAt(0);
			anchorable.Recycle();
		}
	}
}
