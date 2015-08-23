using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	[SerializeField] private float rotationSpeed = 1;

	private void Update() {
		float oldRotation = transform.eulerAngles.z;
		float newRotation = oldRotation + rotationSpeed * Time.deltaTime;
		transform.eulerAngles = new Vector3(0, 0, newRotation);
	}
}
