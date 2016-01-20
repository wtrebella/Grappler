using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
public class CooldownLabel : MonoBehaviour {
	[SerializeField] private GrapplingState grappling;
	[SerializeField] int totalNumBars = 27;

	private UILabel label;
	private int previousNumBars;

	private void Awake() {
		label = GetComponent<UILabel>();
	}

	private void Update() {
		UpdateLabel();
	}

	private void UpdateLabel() {
		float percentage = grappling.GetCooldownCompletionPercentage();
		int numBars = (int)(percentage * totalNumBars);
		numBars = Mathf.Clamp(numBars, 1, numBars);
		if (numBars != previousNumBars) {
			string s = "";
			for (int i = 0; i < numBars; i++) s += "|";
			label.text = s;
			if (numBars == totalNumBars) label.color = Color.green;
			else label.color = Color.red;
		}
		previousNumBars = numBars;
	}
}
