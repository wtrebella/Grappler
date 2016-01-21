using UnityEngine;
using System.Collections;


public class Trail : MonoBehaviour {
	[SerializeField] private Material trailMat;
	[SerializeField] private KickingState kickingState;

	private float initialHue;
	private HSVColor currentColor;

	public void Kick() {
		StartCoroutine(KickCoroutine());
	}

	private IEnumerator KickCoroutine() {
		HSVColor color = currentColor;
		color.h = initialHue;
		color.s = 1;
		color.v = 1;
		color.a = 0.5f;

		float timer = kickingState.GetKickDuration();
		while (timer > 0) {
			float step = 0.5f * Time.deltaTime;
			color.h += step;
			if (color.h >= 1) color.h -= 1;
			color.s = Mathf.Clamp(color.s -= step, 0.5f, 1f);
			color.v = Mathf.Clamp01(color.v += Time.deltaTime);
			timer -= Time.deltaTime;
			Color newColor = color.HSVToRGB();
			trailMat.SetColor("_Color", newColor);
			currentColor = color;
			yield return null;
		}

		Go.to(trailMat, 0.2f, new GoTweenConfig().materialColor(Color.white));
	}

	private void Awake() {
		trailMat.color = Color.white;
		initialHue = new HSVColor(trailMat.color).h;
	}

	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
