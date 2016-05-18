using UnityEngine;
using System.Collections;

public class FlippingStateController : PlayerStateController {
	[SerializeField] private float flipSpeed = 10;
	[SerializeField] private Rigidbody2D body;

	private void Awake() {
		BaseAwake();
		state = Player.PlayerStates.Flipping;
	}

	public override void EnterState() {
		base.EnterState();

//		body.constraints = RigidbodyConstraints2D.None;
	}

	public override void ExitState() {
		base.ExitState();

		SetRotation(0);
//		body.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	public override void RightTouchDown() {
		base.RightTouchDown();

		Grapple();
	}

	public override void LeftTouchUp() {
		base.LeftTouchDown();

		SetToFallingState();
	}

	public override void FixedUpdateState() {
		UpdateFlip();
	}

	private void UpdateFlip() {
		float targetRotation = GetRotation() + flipSpeed * Time.fixedDeltaTime;
		SetRotation(targetRotation);
	}

	private float GetRotation() {
		return body.transform.eulerAngles.z;
	}

	private void SetRotation(float targetRotation) {
		float curRotation = GetRotation();
		float newRotation = Mathf.LerpAngle(curRotation, targetRotation, 1);
		body.transform.eulerAngles = new Vector3(0, 0, newRotation);
	}
}
