using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AirTimeText : MonoBehaviour {
	[SerializeField] private Text text;

	public void OnAirTimeIncreased(float airTime) {
		text.text = "Air Time: " + airTime.ToString("0.0");
	}

	public void OnAirTimeReset() {
		text.text = "Air Time: 0.0";
	}
}
