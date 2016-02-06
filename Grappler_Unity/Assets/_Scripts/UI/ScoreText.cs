using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
	[SerializeField] private Transform objectToMeasure;

	private Text label;
	private float dist;
	private float startOffset;

	private void Awake() {
		label = GetComponent<Text>();
		startOffset = -objectToMeasure.position.x;
	}

	private void Update() {
		dist = objectToMeasure.position.x + startOffset;
		label.text = ((int)(dist / 10)).ToString();
	}
}
