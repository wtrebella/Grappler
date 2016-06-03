using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private MeshRenderer sprite;

	private bool isShowing = false;

	private void Awake() {
		Hide();
	}

	public void Show() {
		UpdateValues();
		sprite.enabled = true;
		isShowing = true;
	}

	public void Hide() {
		sprite.enabled = false;
		isShowing = false;
	}

	private void Update() {
		if (!isShowing) return;

		UpdateValues();
	}

	private void UpdateValues() {
		transform.position = rigid.transform.position;
		Vector2 velocity = rigid.velocity;
		Vector2 direction = velocity.normalized;
		float angle = WhitTools.DirectionToAngle(direction);
		Vector3 eulerAngles = new Vector3(0, 0, angle);
		transform.eulerAngles = eulerAngles;
	}
}
