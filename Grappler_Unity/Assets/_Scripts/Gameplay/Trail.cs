using UnityEngine;
using System.Collections;


public class Trail : MonoBehaviour {
	[SerializeField] private Material trailMat;
	[SerializeField] private float alpha;

	private float initialHue;
	private HSVColor currentColor;

	public void SetTrailColor(Color color) {
		trailMat.SetColor("_Color", color);
	}

	private void Awake() {
		SetTrailColor(new Color(1, 1, 1, alpha));
		initialHue = new HSVColor(trailMat.color).h;
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
