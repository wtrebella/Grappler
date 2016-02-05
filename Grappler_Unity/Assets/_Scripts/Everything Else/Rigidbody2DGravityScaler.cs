using UnityEngine;
using System.Collections;
using System;

public class Rigidbody2DGravityScaler : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;

	private float initialGravityScale;

	public void TweenGravityScale(float toScale, float duration, Action<AbstractGoTween> onComplete) {
		Go.to(rigid, duration, new GoTweenConfig().floatProp("gravityScale", toScale).onComplete(onComplete));
	}

	public void ResetToInitialGravityScale() {
		SetGravityScale(initialGravityScale);
	}

	private void Awake() {
		initialGravityScale = rigid.gravityScale;
	}

	private void SetGravityScale(float scale) {
		rigid.gravityScale = scale;
	}
}
