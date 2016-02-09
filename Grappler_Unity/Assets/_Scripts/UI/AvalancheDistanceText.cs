using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AvalancheDistanceText : MonoBehaviour {
	private Text label;

	private void Awake() {
		label = GetComponent<Text>();
	}

	public void HandleDistanceChanged(float distance) {
		distance = Mathf.Max(0, distance);
		label.text = (distance / 10).ToString("0.0");
	}
}
