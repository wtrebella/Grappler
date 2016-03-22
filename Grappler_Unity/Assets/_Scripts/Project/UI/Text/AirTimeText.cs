using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AirTimeText : MonoBehaviour {
	[SerializeField] private Text text;

	public void OnAirTimeIncreased(float airTime) {
		text.text = "AIR TIME: " + airTime.ToString("0.0");
	}

	public void OnAirTimeReset() {
		text.text = "AIR TIME: 0.0";
	}
}
