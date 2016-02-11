using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class RockSlideDistanceText : MonoBehaviour {
	private Text label;

	private void Awake() {
		label = GetComponent<Text>();
	}

	public void HandleDistanceChanged(float distance) {
		distance = Mathf.Max(0, distance);
		label.text = "ROCK SLIDE DISTANCE: " + (distance / 20.0f).ToString("0.0");
	}
}
