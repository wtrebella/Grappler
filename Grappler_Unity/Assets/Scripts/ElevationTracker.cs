using UnityEngine;
using System.Collections;

public class ElevationTracker : MonoBehaviour {
	[SerializeField] private UILabel elevationLabel;
	[SerializeField] private UILabel bestElevationLabel;

	private float bestElevation = 0;

	private void Update() {
		float elevation = GetElevationValue();
		bestElevation = Mathf.Max(elevation, bestElevation);
		elevationLabel.text = ConvertElevationValueToString(elevation);
		bestElevationLabel.text = ConvertBestElevationValueToString(bestElevation);
	}

	private string ConvertElevationValueToString(float value) {
		return "Elevation: " + value.ToString("0.0");
	}

	private string ConvertBestElevationValueToString(float value) {
		return "Score: " + value.ToString("0.0");
	}

	private float GetElevationValue() {
		float elevation = transform.position.y;
		elevation = Mathf.Max(0, elevation);
		return elevation;
	}
}
