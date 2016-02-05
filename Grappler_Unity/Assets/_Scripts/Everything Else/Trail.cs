using UnityEngine;
using System.Collections;


public class Trail : MonoBehaviour {
	[SerializeField] private Material trailMat;
	[SerializeField] private KickingState kickingState;
	[SerializeField] private float alpha;

	private float initialHue;
	private HSVColor currentColor;

	public void Kick() {
		StartCoroutine(DoKickTrailColor());
	}

	private IEnumerator DoKickTrailColor() {
		HSVColor color = currentColor;
		color.h = initialHue;
		color.s = 1;
		color.v = 1;
		color.a = alpha;

		float timer = kickingState.GetKickDuration();
		while (timer > 0) {
			float step = 0.5f * Time.deltaTime;
			color.h += step;
			if (color.h >= 1) color.h -= 1;
			color.s = Mathf.Clamp(color.s -= step, 0.5f, 1f);
			color.v = Mathf.Clamp01(color.v += Time.deltaTime);
			timer -= Time.deltaTime;
			Color newColor = color.HSVToRGB();
			SetTrailColor(newColor);
			currentColor = color;
			yield return null;
		}

		Go.to(trailMat, 0.2f, new GoTweenConfig().materialColor(new Color(1, 1, 1, alpha)));
	}

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
