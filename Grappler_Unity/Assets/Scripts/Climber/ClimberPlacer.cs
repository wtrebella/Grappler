using UnityEngine;
using System.Collections;

public class ClimberPlacer : MonoBehaviour {
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	[SerializeField] private float sizeOfRotationCheck = 0.05f;
	[SerializeField] private float smoothTime = 0.1f;
	[SerializeField] private float speed = 0.01f;

	private Vector3 positionVelocity;
	private float rotationVelocity;
	private float placeOnChunk = 0;
	private MountainChunk chunk;

	private void Awake() {
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk mountainChunk) {
		if (chunk != null) return;
		chunk = mountainChunk;
	}

	private void PlaceOnMountainChunk(MountainChunk mountainChunk, float place) {
		if (place == placeOnChunk) return;
		placeOnChunk = place;
		transform.position = Vector3.SmoothDamp(transform.position, mountainChunk.GetPositionAlongLine(placeOnChunk), ref positionVelocity, smoothTime);
		float rot = GetRotationAtPlace(chunk, placeOnChunk);
		transform.eulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(transform.eulerAngles.z, rot, ref rotationVelocity, smoothTime));
	}

	private float GetRotationAtPlace(MountainChunk chunk, float place) {
		float minPlace = Mathf.Clamp01(place - sizeOfRotationCheck / 2f);
		float maxPlace = Mathf.Clamp01(place + sizeOfRotationCheck / 2f);
		Vector2 minPosition = chunk.GetPositionAlongLine(minPlace);
		Vector2 maxPosition = chunk.GetPositionAlongLine(maxPlace);
		Vector2 vector = maxPosition - minPosition;
		float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90;

		return angle;
	}

	private void Update() {
		if (chunk == null) return;

		PlaceOnMountainChunk(chunk, placeOnChunk + speed * Time.deltaTime);
	}
}
