using UnityEngine;
using System.Collections;

public class ClimberPlacer : MonoBehaviour {
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	[SerializeField] private float sizeOfRotationCheck = 0.05f;
	[SerializeField] private float smoothTime = 0.1f;
	[SerializeField] private float speed = 0.01f;

	private Vector3 positionVelocity;
	private float rotationVelocity;
	private float placeOnMountain = 0;

	private void PlaceOnMountainChunks(float place) {
		if (place == placeOnMountain) return;
		placeOnMountain = place;
		MountainChunk chunk = mountainChunkGenerator.GetMountainChunkAtDist(place);
		float placeOnChunk = placeOnMountain - mountainChunkGenerator.GetMountainChunkNumAtDist(place);
		Vector3 position = Vector3.SmoothDamp(transform.position, chunk.GetPositionAlongLine(placeOnChunk), ref positionVelocity, smoothTime);
		Vector3 rotation = new Vector3(0, 0, Mathf.SmoothDampAngle(transform.eulerAngles.z, GetRotationAtPlace(chunk, placeOnChunk), ref rotationVelocity, smoothTime));
		transform.position = position;
		transform.eulerAngles = rotation;
	}

	private float GetRotationAtPlace(MountainChunk chunk, float place) {
		float minPlace = place - sizeOfRotationCheck / 2f;
		float maxPlace = place + sizeOfRotationCheck / 2f;
		Vector2 minPosition = chunk.GetPositionAlongLine(minPlace);
		Vector2 maxPosition = chunk.GetPositionAlongLine(maxPlace);
		Vector2 vector = maxPosition - minPosition;
		float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90;

		return angle;
	}

	private void Update() {
		PlaceOnMountainChunks(placeOnMountain + speed * Time.deltaTime);
	}
}
