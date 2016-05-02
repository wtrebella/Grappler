using UnityEngine;
using System.Collections;

public class SkatingController : MonoBehaviour {
	[SerializeField] private Player player;
	[SerializeField] private float speed = 10;
	[SerializeField] private float verticalIceOffset = 0.9f;

	private Ice ice;
	private bool isSkating = false;

	public void SetIce(Ice ice) {
		this.ice = ice;
	}

	public void StartSkating() {
		ApplyIceOffset();
		ResetRotation();
		isSkating = true;
	}

	public void StopSkating() {
		isSkating = false;
	}

	private void ApplyIceOffset() {
		Vector3 position = player.body.transform.position;
		position.y = ice.transform.position.y + verticalIceOffset;
		player.body.transform.position = position;
	}

	private void FixedUpdate() {
		if (!isSkating) return;

		Vector3 position = player.body.transform.position;
		position.x += speed * Time.deltaTime * Time.timeScale;
		player.body.transform.position = position;
	}

	private void ResetRotation() {
		player.body.transform.localRotation = Quaternion.identity;
		player.feet.transform.localRotation = Quaternion.identity;
	}
}