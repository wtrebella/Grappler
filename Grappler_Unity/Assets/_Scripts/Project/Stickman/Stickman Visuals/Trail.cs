using UnityEngine;
using System.Collections;


public class Trail : MonoBehaviour {
	public Color trailColor {
		get {return trailMat.color;}
		set {SetTrailColor(value);}
	}

	[SerializeField] private Material trailMat;
	[SerializeField] private float alpha;
	[SerializeField] private float burstSpeed = 1;

	private bool isBursting;
	private HSVColor currentColorHSV;

	public void BurstTrailColor(Color color) {
		isBursting = true;
		currentColorHSV = WadeUtils.RGBToHSV(color);
	}

	private void SetTrailColor(Color color) {
		trailMat.SetColor("_Color", new Color(color.r, color.g, color.b, alpha));
	}

	private void Awake() {
		SetTrailColor(Color.white);
	}

	private void Start() {
	
	}
	
	private void Update() {
		if (isBursting) RunBurst();
	}

	private void RunBurst() {
		float s = currentColorHSV.s;
		float v = currentColorHSV.v;
		s = Mathf.Max(0.0f, s - Time.deltaTime * burstSpeed);
		v = Mathf.Min(1.0f, v + Time.deltaTime * burstSpeed);
		currentColorHSV.s = s;
		currentColorHSV.v = v;
		if (Mathf.Approximately(s, 0.0f) && Mathf.Approximately(v, 1.0f)) {
			currentColorHSV.s = 0.0f;
			currentColorHSV.v = 1.0f;
			isBursting = false;
		}
		Color currentColor = WadeUtils.HSVToRGB(currentColorHSV);
		trailColor = currentColor;
	}
}
